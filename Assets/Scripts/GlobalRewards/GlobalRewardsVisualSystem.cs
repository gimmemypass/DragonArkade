using System;
using Commands;
using Components;
using HECSFramework.Core;
using Systems.GlobalRewards;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Rewards, Doc.Visual, "Visual system for rewards for player")]
    public sealed class GlobalRewardsVisualSystem : BaseSystem,
        IReactGlobalCommand<ApplyRewardVisualCommand>, IReactCommand<ApplyRewardVisualCommand>, IGlobalStart
    {
        [Required] public SoftCurrencyRewardVisualConfigComponent SoftCurrencyRewardVisualConfigComponent;

        private SoftCurrencyRewardAnimation softCurrencyRewardAnimation;

        public override void InitSystem()
        {
        }

        public void GlobalStart()
        {
            softCurrencyRewardAnimation = new SoftCurrencyRewardAnimation(SoftCurrencyRewardVisualConfigComponent.CollectConfig);
        }

        public void CommandGlobalReact(ApplyRewardVisualCommand command)
        {
            if (command.Delayed) //TODO refactor
                return;
            ProcessVisualCommand(command);
        }

        public void CommandReact(ApplyRewardVisualCommand command)
        {
            ProcessVisualCommand(command);
        }

        private void ProcessVisualCommand(ApplyRewardVisualCommand command)
        {
            switch (command.CounterId)
            {
                case CounterIdentifierContainerMap.SoftValue:

                    if (command.RewardAmount == 0)
                    {
                        Debug.Log("we dont have reward amount");
                        return;
                    }

                    softCurrencyRewardAnimation.ApplyRewardAnimationFrom(command);
                    break;
            }
        }
    }
}