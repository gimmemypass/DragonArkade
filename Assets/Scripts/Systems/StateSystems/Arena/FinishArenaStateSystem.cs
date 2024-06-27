using System;
using System.Threading.Tasks;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using UnityEngine.SceneManagement;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameState, "finish arena state")]
    public sealed class FinishArenaStateSystem : BaseGameStateSystem, IGlobalStart
    {
        private LevelsHolderComponent levelsHolderComponent;
        private UISystem uiSystem;
        private EntitiesFilter aiFilter;

        public override void InitSystem()
        {
            aiFilter = EntityManager.Default.GetFilter(Filter.Get<AIStrategyComponent, FactionComponent>());
        }

        public void GlobalStart()
        {
            levelsHolderComponent = EntityManager.GetSingleComponent<LevelsHolderComponent>();
            uiSystem = EntityManager.Default.GetSingleSystem<UISystem>();
        }

        protected override int State { get; } = GameStateIdentifierMap.FinishArena;

        protected override void ProcessState(int from, int to)
        {
            ProcessStateAsync().Forget();
        }

        private async UniTask ProcessStateAsync()
        {
            foreach (var ai in aiFilter)
            {
                ai.Command(new ForceStopAICommand());
            }

            await ShowEmptyGroup();
            var arenaFinishComponent = Owner.GetComponent<ArenaFinishComponent>();
            if (arenaFinishComponent.WinnerFaction == FactionIdentifierMap.PlayerFactionIdentifier)
            {
                var finalScreen = await uiSystem.ShowUI(UIIdentifierMap.FinalScreen_UIIdentifier, needInit: false);
                finalScreen.GetComponent<FinalLevelScreenComponent>().Reward = levelsHolderComponent.ArenaWinReward;
                finalScreen.Init();
            }
            else
            {
                var finalScreen = await uiSystem.ShowUI(UIIdentifierMap.LoseScreen_UIIdentifier, needInit: false);
                finalScreen.GetComponent<FinalLevelScreenComponent>().Reward = levelsHolderComponent.ArenaLoseReward;
                finalScreen.Init();
            }
        }

        private UniTask ShowEmptyGroup()
        {
            return uiSystem.ShowUIGroup(new UIGroupCommand()
                { Show = true, UIGroup = UIGroupIdentifierMap.EmptyGroup });
        }
    }
}