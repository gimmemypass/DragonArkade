using System;
using BluePrints.Identifiers;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Spawn, "move enemy to spawn point")]
    public sealed class MoveEnemyToSpawnPointSystem : BaseSystem, IHaveActor, IAfterEntityInit
    {
        [Required] public UnityTransformComponent UnityTransformComponent;
        [Required] public RigidbodyProviderComponent RigidbodyProviderComponent;
        
        private EntitiesFilter spawnPoints;
        public Actor Actor { get; set; }

        public override void InitSystem()
        {
        }

        public void AfterEntityInit()
        {
            Owner.GetOrAddComponent<TargetPositionComponent>().Pos = UnityTransformComponent.Transform.position;
            Rigidbody rigidbody = RigidbodyProviderComponent.Get;
            rigidbody.isKinematic = true;
            MoveToSpawnPoint();
        }

        private void MoveToSpawnPoint()
        {
            Actor.TryGetComponent(out EnemyAttachToSpawnPointMonoComponent monoComponent);
            spawnPoints = EntityManager.Default.GetFilter<SpawnPointComponent>();
            spawnPoints.ForceUpdateFilter();
            foreach (var spawnPoint in spawnPoints)
            {
                if (spawnPoint.GetComponent<SpawnPointComponent>().SpawnPointIdentifier.Id ==
                    monoComponent.SpawnPointIdentifier.Id)
                {
                    UnityTransformComponent.Transform.position =
                        spawnPoint.GetComponent<UnityTransformComponent>().Transform.position;
                    break;
                }
            }
        }
    }
}