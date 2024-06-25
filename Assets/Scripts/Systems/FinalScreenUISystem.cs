using System;
using Commands;
using Components;
using Components.MonoBehaviourComponents;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine.SceneManagement;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class FinalScreenUISystem : BaseSystem, IHaveActor
    {
        [Required] public FinalLevelScreenComponent FinalLevelScreenComponent;
        public Actor Actor { get; set; }
        private FinalScreenMonoComponent monoComponent;
        
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
            monoComponent.ResetButton.onClick.AddListener(() => Reset().Forget());
            
            var reward = FinalLevelScreenComponent.Reward;
            monoComponent.SoftValueReward.text = $"{reward.ToString()}";
        }

        public override void Dispose()
        {
            monoComponent.ResetButton.onClick.RemoveAllListeners();
        }

        private async UniTask Reset()
        {
            monoComponent.ResetButton.interactable = false;
            monoComponent.SoftValueReward.text = "0";
            EntityManager.Default.Command(new ApplyRewardVisualCommand()
            {
                CounterId = CounterIdentifierContainerMap.SoftValue,
                RewardAmount = FinalLevelScreenComponent.Reward,
                Delayed = false,
                Sender = null,
                CanvasId = AdditionalCanvasIdentifierMap.TopCanvas,
                DelayedStateId = 0,
                HideFX = false,
            });
            await UniTask.Delay(1000);
            EntityManager.Command(new SaveCommand());
            SceneManager.LoadScene("Root");
        }
    }
}