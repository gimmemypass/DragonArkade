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
    [Documentation(Doc.Abilities, "add sphere ability")]
    public sealed class AddSphereAbilitySystem : BaseCardAbilitySystem, IReactCommand<AnimationEventCommand>, IGlobalStart
    {
        private const float OFFSET = 0.5f;
        private int toAdd = 0;
        private EntitiesFilter spheresFilter;
        private EntitiesFilter spawnPointsFilter;
        private CharactersHolderComponent charactersHolderComponent;

        public override void InitSystem()
        {
            spheresFilter = EntityManager.Default.GetFilter<SphereComponent>();
            spawnPointsFilter = EntityManager.Default.GetFilter<SpawnPointComponent>();
        }
        public void GlobalStart()
        {
            charactersHolderComponent = EntityManager.Default.GetSingleComponent<CharactersHolderComponent>();
        }
        public override UniTask ExecuteCard(Entity owner = null, Entity target = null, bool enable = true)
        {
            toAdd++;
            if(target != null && target.ContainsMask<AnimatorStateComponent>())
                target.Command(new TriggerAnimationCommand(){Index = AnimParametersMap.Ball_Appear});
            else
                AddAllSpheres();
            return UniTask.CompletedTask;
        }

        public void CommandReact(AnimationEventCommand command)
        {
            if (command.Id != AnimationEventIdentifierMap.CreateBall) return;

            AddAllSpheres();
        }

        private void AddAllSpheres()
        {
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
            var sphereContainer = charactersHolderComponent.SphereContainer;

            spawnPointsFilter.ForceUpdateFilter();
            var spawnPoint = spawnPointsFilter.FirstOrDefault(a =>
                    a.GetComponent<SpawnPointComponent>().SpawnPointIdentifier.Id ==
                    SpawnPointIdentifierMap.SphereSpawnPointIdentifier)
                .GetComponent<UnityTransformComponent>()
                .Transform.position;
            var position = spawnPoint;

            foreach (var sph in spheresFilter)
            {
                var offset = (spawnPoint - sph.GetComponent<UnityTransformComponent>().Transform.position).normalized * OFFSET /
                             spheresFilter.Count;
                if (offset == Vector3.zero)
                    offset = Vector3.up;
                position += offset;
            }

            var sphere = await sphereContainer.GetActor(position: position);
            sphere.Init();
            foreach (var sphereEnt in spheresFilter)
            {
                sphere.GetHECSComponent<SphereComponent>().Dir = sphereEnt.GetComponent<SphereComponent>().Dir;
                sphere.GetHECSComponent<SpeedComponent>().SetValue(sphereEnt.GetComponent<SpeedComponent>().Value);
                break;
            }
        }


    }
}