using System;
using System.Threading.Tasks;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameState, "game state controller for start menu")]
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
        protected override void ProcessState(int from, int to)
        {
            ProcessStateAsync().Forget();
        }

        private async UniTask ProcessStateAsync()
        {
            await EntityManager.Default.GetSingleSystem<SceneManagerSystem>().LoadScene(scenesHolderComponent.Customization);
            //
            var mainChar = EntityManager.Default.GetSingleComponent<MainCharacterTagComponent>().Owner;
            mainChar.Command(new FloatAnimationCommand() { Index = AnimParametersMap.IdleBlend, Value = 1f });
            EntityManager.Default.Command(new UIGroupCommand()
                { Show = true, UIGroup = UIGroupIdentifierMap.StartScreenGroup});
        }

    }
}