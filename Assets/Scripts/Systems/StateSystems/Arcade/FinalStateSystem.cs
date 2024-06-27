using System;
using System.Threading.Tasks;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameState, "final state")]
    public sealed class FinalStateSystem : BaseGameStateSystem, IGlobalStart
    {
        private PlayerLevelComponent playerLevelComponent;
        private LevelsHolderComponent levelsHolderComponent;
        private EntitiesFilter deadMainCharacter;
        private UISystem uiSystem;

        public override void InitSystem()
        {
            deadMainCharacter =
                EntityManager.Default.GetFilter(Filter.Get<MainCharacterTagComponent, IsDeadTagComponent>());
        }

        public void GlobalStart()
        {
            playerLevelComponent = EntityManager.GetSingleComponent<PlayerLevelComponent>();
            levelsHolderComponent = EntityManager.GetSingleComponent<LevelsHolderComponent>();
            uiSystem = EntityManager.Default.GetSingleSystem<UISystem>();
        }

        protected override int State { get; } = GameStateIdentifierMap.Final;

        protected override void ProcessState(int from, int to)
        {
            ProcessStateAsync().Forget();
        }

        private async UniTask ProcessStateAsync()
        {
            await ShowEmptyGroup();
            var levelData = levelsHolderComponent.LevelDatas[playerLevelComponent.Level];
            if (deadMainCharacter.Count != 0)
            {
                playerLevelComponent.WaveNumber = 0;
                var finalScreen = await uiSystem.ShowUI(UIIdentifierMap.LoseScreen_UIIdentifier, needInit: false);
                finalScreen.GetComponent<FinalLevelScreenComponent>().Reward = levelData.LoseReward;
                finalScreen.Init();
                return;
            }

            if (levelData.Waves.Length >
                playerLevelComponent.WaveNumber + 1)
            {
                playerLevelComponent.WaveNumber++;
                EntityManager.Command(new ForceGameStateTransitionGlobalCommand()
                    { GameState = GameStateIdentifierMap.PrepareWave });
            }
            else
            {
                var finalScreen = await uiSystem.ShowUI(UIIdentifierMap.FinalScreen_UIIdentifier, needInit: false);
                finalScreen.GetComponent<FinalLevelScreenComponent>().Reward = levelData.WinReward;
                finalScreen.Init();

                playerLevelComponent.WaveNumber = 0;
                if (levelsHolderComponent.LevelDatas.Count > playerLevelComponent.Level + 1)
                    playerLevelComponent.Level++;
            }
        }

        private UniTask ShowEmptyGroup()
        {
            return uiSystem.ShowUIGroup(new UIGroupCommand()
                { Show = true, UIGroup = UIGroupIdentifierMap.EmptyGroup });
        }
    }
}