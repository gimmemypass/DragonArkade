using System;
using System.Threading.Tasks;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class AddSphereAbilitySystem : BaseCardAbilitySystem, IReactCommand<AnimationEventCommand>
    {
        private const float OFFSET = 0.5f;
        private int toAdd = 0;
        public override void InitSystem()
        {
        }

        public override UniTask ExecuteCard(Entity owner = null, Entity target = null, bool enable = true)
        {
            target?.Command(new TriggerAnimationCommand(){Index = AnimParametersMap.Ball_Appear});
            toAdd++;
            return UniTask.CompletedTask;
        }

        public void CommandReact(AnimationEventCommand command)
        {
            if (command.Id != AnimationEventIdentifierMap.CreateBall) return;
            
            for (int i = 0; i < toAdd; i++)
            {
                AddSphere().Forget();
            }
        }

        private async UniTask AddSphere()
        {
            if (toAdd <= 0)
                throw new Exception();
            toAdd--;
            var charactersHolderComponent = EntityManager.Default.GetSingleComponent<CharactersHolderComponent>();
            var sphereContainer = charactersHolderComponent.SphereContainer;
            var spheres = EntityManager.Default.GetFilter<SphereComponent>();

            var spawnPoint = EntityManager.Default.GetFilter<SpawnPointComponent>().FirstOrDefault(a =>
                    a.GetComponent<SpawnPointComponent>().SpawnPointIdentifier.Id ==
                    SpawnPointIdentifierMap.SphereSpawnPointIdentifier)
                .GetComponent<UnityTransformComponent>()
                .Transform.position;
            var position = spawnPoint;

            foreach (var sph in spheres)
            {
                var offset = (spawnPoint - sph.GetComponent<UnityTransformComponent>().Transform.position).normalized * OFFSET /
                             spheres.Count;
                if (offset == Vector3.zero)
                    offset = Vector3.up;
                position += offset;
            }

            var sphere = await sphereContainer.GetActor(position: position);
            sphere.Init();
            foreach (var sphereEnt in EntityManager.Default.GetFilter<SphereComponent>())
            {
                sphere.GetHECSComponent<SphereComponent>().Dir = sphereEnt.GetComponent<SphereComponent>().Dir;
                sphere.GetHECSComponent<SpeedComponent>().SetValue(sphereEnt.GetComponent<SpeedComponent>().Value);
                break;
            }
        }
    }
}