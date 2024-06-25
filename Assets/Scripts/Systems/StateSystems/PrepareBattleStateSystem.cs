using System;
using System.Threading.Tasks;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameState, "")]
    public sealed class PrepareBattleStateSystem : BaseGameStateSystem, IGlobalStart
    {
        protected override int State { get; } = GameStateIdentifierMap.PrepareBattle;
        private GameStateComponent gameStateComponent;
        private LevelsHolderComponent levelsHolderComponent;
        private PlayerLevelComponent playerLevelComponent;
        private ScenesHolderComponent scenesHolderComponent;

        public override void InitSystem()
        {
        }

        public void GlobalStart()
        {
            AsSingle(ref gameStateComponent);
            AsSingle(ref levelsHolderComponent);
            AsSingle(ref playerLevelComponent);
            AsSingle(ref scenesHolderComponent);
        }

        protected override async void ProcessState(int from, int to)
        {
            //load scene
            await EntityManager.Default.GetSingleSystem<SceneManagerSystem>().LoadScene(scenesHolderComponent.Battle); 
            //

            SpawnSphere();
            LevelData levelData = levelsHolderComponent.LevelDatas[playerLevelComponent.Level];
            Object.Instantiate(levelData.Location);
            EndState();
        }

        private void SpawnSphere()
        {
            var spheresController = EntityManager.Default.GetSingleComponent<SpheresControllerTagComponent>().Owner;
            spheresController.Command(new ExecuteAbilityByIDCommand
            {
                AbilityIndex = AbilitiesMap.AddSphereAbility,
                Owner = Owner,
                Target = spheresController,
                Enable = true
            });
        }
    }
}