using System;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using HECSFramework.Unity;
using Random = UnityEngine.Random;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameState, "")]
    public sealed class BattleStateSystem : BaseGameStateSystem, IUpdatable, IGlobalStart
    {
        protected override int State { get; } = GameStateIdentifierMap.BattleState;
        private GameStateComponent gameStateComponent;
        private EntitiesFilter enemies;
        private EntitiesFilter deadMainCharacter;

        public override void InitSystem()
        {
            enemies = EntityManager.Default.GetFilter<EnemyTagComponent>();
            deadMainCharacter =
                EntityManager.Default.GetFilter(Filter.Get<MainCharacterTagComponent, IsDeadTagComponent>());
        }

        public void GlobalStart()
        {
            AsSingle(ref gameStateComponent);
        }

        protected override void ProcessState(int from, int to)
        {
            EntityManager.Default.Command(new UIGroupCommand()
            {
                UIGroup = UIGroupIdentifierMap.BattleGroup,
                Show = true
            });

        }

        public void UpdateLocal()
        {
            if (gameStateComponent.CurrentState != GameStateIdentifierMap.BattleState)
                return;
            if (enemies.Count == 0 || deadMainCharacter.Count != 0)
            {
                EntityManager.Default.Command(new ForceGameStateTransitionGlobalCommand(){GameState = GameStateIdentifierMap.Final});
            }
        }
    }
}