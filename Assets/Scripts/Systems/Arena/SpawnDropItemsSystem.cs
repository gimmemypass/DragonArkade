using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Cysharp.Threading.Tasks;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, Doc.Arena, "spawn drop items")]
    public sealed class SpawnDropItemsSystem : BaseSystem, IUpdatable, IReactGlobalCommand<TransitionGameStateCommand>, IGlobalStart
    {
        [Required] public SpawnDropItemsComponent SpawnDropItemsComponent;
        private ItemsGlobalHolderComponent itemsGlobalHolderComponent;
        private Entity[] spawnPoints;
        private IEnumerator spawnPointsGenerator;
        private float cooldown;
        private int droppedItems;

        private GameStateComponent gameStateComponent;
        private EntitiesFilter spawnPointsFilter;

        public override void InitSystem()
        {
            spawnPointsFilter = EntityManager.Default.GetFilter<SpawnPointComponent>();
        }

        public void GlobalStart()
        {
            AsSingle(ref gameStateComponent);
            AsSingle(ref itemsGlobalHolderComponent);
        }

        public void CommandGlobalReact(TransitionGameStateCommand command)
        {
            if (command.To != GameStateIdentifierMap.Arena)
                return;
            spawnPointsFilter.ForceUpdateFilter();
            spawnPoints = spawnPointsFilter.ToArray().Where(a =>
                a.GetComponent<SpawnPointComponent>().SpawnPointIdentifier.Id ==
                SpawnPointIdentifierMap.DropItemSpawnPoint).ToArray();
        }

        public void UpdateLocal()
        {
            if (!gameStateComponent.IsNeededState(GameStateIdentifierMap.Arena))
                return;
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                return;
            }
            cooldown = SpawnDropItemsComponent.Cooldown;
            DropItem(GetNextSpawnPoint()).Forget();
        }

        private async UniTask DropItem(Entity spawnPoint)
        {
            droppedItems++;
            var vector = Vector3.forward;
            vector *= SpawnDropItemsComponent.Radius;
            var angle = (droppedItems - droppedItems % spawnPoints.Length) * SpawnDropItemsComponent.AngleStep;
            vector = Quaternion.Euler(0, angle, 0) * vector;
            var pos = spawnPoint.GetComponent<UnityTransformComponent>().Transform.position +vector;
            var drop = itemsGlobalHolderComponent.GetDropByContainerId(EntityContainersMap.DropFireballContainer);
            var actor = await drop.GetActor(position: pos);
            actor.Init();
        }

        private Entity GetNextSpawnPoint()
        {
            if (spawnPointsGenerator == null || !spawnPointsGenerator.MoveNext())
            {
                spawnPointsGenerator = spawnPoints.GetEnumerator();
                spawnPointsGenerator.MoveNext();
            }
            return (Entity)spawnPointsGenerator.Current;
        }
    }
}