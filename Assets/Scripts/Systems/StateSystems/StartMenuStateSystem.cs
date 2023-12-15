using System;
using Commands;
using Components;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class StartMenuStateSystem : BaseGameStateSystem, IGlobalStart
    {
        private ScenesHolderComponent scenesHolderComponent;
        private PlayerLevelComponent playerLevelComponent;
        public override void InitSystem()
        {
        }

        public void GlobalStart()
        {
            AsSingle(ref scenesHolderComponent);
            AsSingle(ref playerLevelComponent);
        }

        protected override int State { get; } = GameStateIdentifierMap.StartMenu;
        protected override async void ProcessState(int from, int to)
        {
            await EntityManager.Default.GetSingleSystem<SceneManagerSystem>().LoadScene(scenesHolderComponent.Customization);
            //
            var mainChar = EntityManager.Default.GetSingleComponent<MainCharacterTagComponent>().Owner;
            mainChar.Command(new FloatAnimationCommand(){Index = AnimParametersMap.IdleBlend, Value = 1f});
            EntityManager.Default.Command(new UIGroupCommand(){Show = true, UIGroup = UIGroupIdentifierMap.StartScreenGroup, OnLoadUI = StartMenuOpened});            
        }

        private void StartMenuOpened()
        {
            if(playerLevelComponent.Level == 0)
                EntityManager.Default.Command(new ShowUICommand(){UIViewType = UIIdentifierMap.Comics_UIIdentifier});
        }
    }
}