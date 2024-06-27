using System;
using Components;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation( Doc.GameState, "arena state")]
    public sealed class ArenaStateSystem : BaseGameStateSystem, IUpdatable, IGlobalStart
    {
        private ItemsGlobalHolderComponent itemsGlobalHolderComponent;
        private GameStateComponent gameStateComponent;
        private Entity mainCharacter;
        private Entity enemy;
        private EntitiesFilter entitiesFilter;
        private EntitiesFilter spawnPointFilter;

        public override void InitSystem()
        {
            entitiesFilter = EntityManager.Default.GetFilter<EnemyTagComponent>();
            spawnPointFilter = EntityManager.Default.GetFilter<SpawnPointComponent>();
        }

        public void GlobalStart()
        {
            AsSingle(ref gameStateComponent);
        }

        protected override int State { get; } = GameStateIdentifierMap.Arena;

        protected override void ProcessState(int from, int to)
        {
        }

        public void UpdateLocal()
        {
            if (!gameStateComponent.IsNeededState(State))
                return;
            // if (FactionHelper.GetHealthSum(FactionIdentifierMap.PlayerFactionIdentifier) <= 0)
            // {
            //     Owner.GetOrAddComponent<ArenaFinishComponent>().WinnerFaction = FactionIdentifierMap.EnemyFactionIdentifier;
            //     EndState();      
            // } 
            // else if (FactionHelper.GetHealthSum(FactionIdentifierMap.EnemyFactionIdentifier) <= 0)
            // {
            //     Owner.GetOrAddComponent<ArenaFinishComponent>().WinnerFaction = FactionIdentifierMap.PlayerFactionIdentifier;
            //     EndState();      
            // }
        }
    }
}