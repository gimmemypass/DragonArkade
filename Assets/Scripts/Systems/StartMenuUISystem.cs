using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class StartMenuUISystem : BaseSystem, IHaveActor, IReactGlobalCommand<LevelUpCommand>
    {
        public Actor Actor { get; set; }
        private StartMenuMonoComponent monoComponent;
        private PlayerLevelComponent playerLevelComponent;
        private ArenaBattleGlobalConfigComponent arenaBattleGlobalConfigComponent;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
            AsSingle(ref arenaBattleGlobalConfigComponent);
            AsSingle(ref playerLevelComponent);
            
            monoComponent.StartButton.onClick.AddListener(StartBattle);
            monoComponent.ArenaButton.onClick.AddListener(StartArenaBattle);
            ArenaButtonUpdateVisual(playerLevelComponent.Level);
        }

        public override void Dispose()
        {
            monoComponent.StartButton.onClick.RemoveListener(StartBattle);
            monoComponent.ArenaButton.onClick.RemoveListener(StartArenaBattle);
        }

        private void StartBattle()
        {
            EntityManager.Default.Command(new ForceGameStateTransitionGlobalCommand(){GameState = GameStateIdentifierMap.PrepareBattle});
        }

        private void StartArenaBattle()
        {
            EntityManager.Default.Command(new ForceGameStateTransitionGlobalCommand(){GameState = GameStateIdentifierMap.PrepareArena});
        }

        public void CommandGlobalReact(LevelUpCommand command)
        {
        }

        private void ArenaButtonUpdateVisual(int currentLevel)
        {
            var arenaIsOpen = currentLevel >= arenaBattleGlobalConfigComponent.OpenAfterLevel;
            monoComponent.ArenaButton.gameObject.SetActive(arenaIsOpen);            
            monoComponent.LockedArenaButton.gameObject.SetActive(!arenaIsOpen);            
        }
    }
}