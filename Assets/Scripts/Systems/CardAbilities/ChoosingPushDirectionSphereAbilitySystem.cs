using System;
using System.Collections.Generic;
using System.Threading;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;
using UnityEngine.UI;
using InvincibleComponent = Components.InvincibleComponent;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ChoosingPushDirectionSphereAbilitySystem : BaseCardAbilitySystem, IReactCommand<InputStartedCommand>, IReactGlobalCommand<TransitionGameStateCommand>
    {
        [Required] public ChoosingPushDirectionSphereAbilityComponent Component;
        private IModifier<float> zeroModifier;
        
        private Entity oneClickWindow;
        private CancellationTokenSource cancellationTokenSource;
        private EntitiesFilter spheresFilter;
        private bool inProcess;

        public override void InitSystem()
        {
            zeroModifier = Component.ResetToZeroModifier.GetModifierWithCast<IModifier<float>>();
            spheresFilter = EntityManager.Default.GetFilter<SphereComponent>();
        }

        public override async UniTask ExecuteCard(Entity owner = null, Entity target = null, bool enable = true)
        {
            //animation
            target!.Command(new TriggerAnimationCommand(){Index = AnimParametersMap.IsAttacking});
            //invincibility
            target.GetOrAddComponent<InvincibleComponent>();
            //hide ui
            EntityManager.Default.Command(new UIGroupCommand()
            {
                Show = true, UIGroup = UIGroupIdentifierMap.EmptyGroup
            });
            
            foreach (var sphere in spheresFilter)
            {
                var speedComponent = sphere.GetComponent<SpeedComponent>();
                speedComponent.AddModifier(Owner.GUID, zeroModifier);
            }

            inProcess = true;
            cancellationTokenSource = new CancellationTokenSource();
            await new RotateSpheresUntilClickJob(cancellationTokenSource.Token, spheresFilter, 300f, Component.AimPrefab).RunJob();

            foreach (var sphere in spheresFilter)
            {
                var speedComponent = sphere.GetComponent<SpeedComponent>();
                speedComponent.RemoveModifier(Owner.GUID, zeroModifier);
            }

            if (EntityManager.Default.GetSingleComponent<GameStateComponent>().CurrentState ==
                GameStateIdentifierMap.BattleState)
            {
                //todo we need ui stack
                //show ui back
                EntityManager.Default.Command(new UIGroupCommand()
                {
                    Show = true, UIGroup = UIGroupIdentifierMap.BattleGroup
                });
            }

            //remove invincibility
            target.RemoveComponent<InvincibleComponent>();
        }

        public void CommandReact(InputStartedCommand command)
        {
            if (command.Index != InputIdentifierMap.Touch)
                return;
            if (!inProcess)
                return;
            inProcess = false;
            cancellationTokenSource.Cancel();           
        }

        public void CommandGlobalReact(TransitionGameStateCommand command)
        {
            if (command.To == GameStateIdentifierMap.Final)
            {
                if (inProcess)
                {
                    inProcess = false;
                    cancellationTokenSource.Cancel();
                }
            }
        }

    }
    
    public struct RotateSpheresUntilClickJob : IHecsJob
    {
        private readonly CancellationToken cancellationToken;
        private readonly EntitiesFilter spheres;
        // private readonly float radius;
        private readonly float speed;
        private float angle;
        private Dictionary<Entity, Transform> sphereToAim;
        private GameObject aimPrefab;

        public RotateSpheresUntilClickJob(CancellationToken cancellationToken, EntitiesFilter spheres, float speed,
            GameObject aimPrefab)
        {
            this.aimPrefab = aimPrefab;
            this.cancellationToken = cancellationToken;
            this.spheres = spheres;
            this.speed = speed;
            angle = 0;
            sphereToAim = new();
        }
        public void Run()
        {
            foreach (var sphere in spheres)
            {
                if(!sphereToAim.ContainsKey(sphere))
                    sphereToAim.Add(sphere, Object.Instantiate(aimPrefab, sphere.AsActor().transform).transform); 
                var aim = sphereToAim[sphere];
                aim.rotation = Quaternion.Euler(angle, 0,0);
                sphere.GetComponent<SphereComponent>().Dir = Quaternion.Euler(angle, 0, 0) * Vector3.up; 
            }
            angle += speed * Time.deltaTime;
        }

        public bool IsComplete()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                foreach (var kv in sphereToAim)
                {
                    Object.Destroy(kv.Value.gameObject);
                }
                sphereToAim.Clear();
                return true;
            }
            return false;
        }
    }
}