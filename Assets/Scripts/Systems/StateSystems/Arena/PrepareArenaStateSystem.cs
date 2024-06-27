using System;
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
    [Documentation(Doc.GameState, "prepare arena state")]
    public sealed class PrepareArenaStateSystem : BaseGameStateSystem, IGlobalStart
    {
        protected override int State { get; } = GameStateIdentifierMap.PrepareArena;
        private ScenesHolderComponent scenesHolderComponent;
        public override void InitSystem() { }
        public void GlobalStart()
        {
            AsSingle(ref scenesHolderComponent);
        }


        protected override void ProcessState(int from, int to)
        {
            ProcessStateAsync().Forget();
        }

        private async UniTask ProcessStateAsync()
        {
            await EntityManager.Default.GetSingleSystem<SceneManagerSystem>().LoadScene(scenesHolderComponent.ArenaBattle);
            EntityManager.Default.Command(new UIGroupCommand()
            {
                Show = true,
                UIGroup = UIGroupIdentifierMap.ArenaBattleGroup
            });
            EndState();
        }
    }
}