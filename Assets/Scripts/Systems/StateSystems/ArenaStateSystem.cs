using System;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using HECSFramework.Unity;
using Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ArenaStateSystem : BaseGameStateSystem, IUpdatable, IGlobalStart
    {
        private ItemsGlobalHolderComponent itemsGlobalHolderComponent;
        private GameStateComponent gameStateComponent;
        private Entity mainCharacter;
        private Entity enemy;
        private EntitiesFilter entitiesFilter;

        public override void InitSystem()
        {
            entitiesFilter = EntityManager.Default.GetFilter<EnemyTagComponent>();
            
        }

        public void GlobalStart()
        {
            AsSingle(ref gameStateComponent);
        }

        protected override int State { get; } = GameStateIdentifierMap.Arena;

        protected override void ProcessState(int from, int to)
        {
            itemsGlobalHolderComponent = EntityManager.Default.GetSingleComponent<ItemsGlobalHolderComponent>();
            mainCharacter = EntityManager.Default.GetSingleComponent<MainCharacterTagComponent>().Owner;
            entitiesFilter.ForceUpdateFilter();
            enemy = entitiesFilter.FirstOrDefault();
            for (int i = 0; i < 6; i++)
            {
                AddToCharacter(mainCharacter).Forget();
                AddToCharacter(enemy).Forget();
            }
        }

        private async UniTask AddToCharacter(Entity character)
        {
            var container = itemsGlobalHolderComponent.GetRandomItem();
            var actor = await container.GetActor();
            actor.Init();
            character.Command(new AddItemToCharacterCommand() { Item = actor.Entity });
        }

        public void UpdateLocal()
        {
            if (!gameStateComponent.IsNeededState(State))
                return;
            if (FactionHelper.GetHealthSum(FactionIdentifierMap.PlayerFactionIdentifier) <= 0)
            {
                Owner.GetOrAddComponent<ArenaFinishComponent>().WinnerFaction = FactionIdentifierMap.EnemyFactionIdentifier;
                EndState();      
            } 
            else if (FactionHelper.GetHealthSum(FactionIdentifierMap.EnemyFactionIdentifier) <= 0)
            {
                Owner.GetOrAddComponent<ArenaFinishComponent>().WinnerFaction = FactionIdentifierMap.PlayerFactionIdentifier;
                EndState();      
            }
        }
    }
}