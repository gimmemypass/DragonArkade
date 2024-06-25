using System;
using System.Linq;
using Commands;
using Components;
using Components.MonoBehaviourComponents;
using HECSFramework.Core;
using HECSFramework.Unity;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, Doc.Visual, "upgrade visual main character system")]
    public sealed class UpgradeVisualMainCharacterSystem : BaseSystem, IReactGlobalCommand<LevelUpCommand>, IGlobalStart, IHaveActor
    {
        public Actor Actor { get; set; }
        private PlayerUpgradeComponent playerUpgradeComponent;
        private VisualByLvlMonoComponent monoComponent;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
        }

        public void GlobalStart()
        {
            AsSingle(ref playerUpgradeComponent);
            UpdateVisual();
        }

        public void CommandGlobalReact(LevelUpCommand command)
        {
            monoComponent.ParticleSystem.Play();
            var level = playerUpgradeComponent.CurrentLevel;
            foreach (var data in monoComponent.Datas)
            {
                if (data.lvl == level)
                {
                    Owner.Command(new TriggerAnimationCommand() { Index = AnimParametersMap.Upgrade });
                    break;
                }
            }
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            DisableAll();
            var level = playerUpgradeComponent.CurrentLevel;
            var needed = monoComponent.Datas[0];
            foreach (var data in monoComponent.Datas)
            {
                if (data.lvl > level)
                    break; 
                needed = data;
            }
            foreach (var element in needed.Elements)
            {
                element.gameObject.SetActive(true);
            }
        }

        private void DisableAll()
        {
            foreach (var data in monoComponent.Datas)
            {
                foreach (var element in data.Elements)
                {
                    element.gameObject.SetActive(false); 
                } 
            }
        }
    }
}