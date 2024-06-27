using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

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


        protected override async void ProcessState(int from, int to)
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