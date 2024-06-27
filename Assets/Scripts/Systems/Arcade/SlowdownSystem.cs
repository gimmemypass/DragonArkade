using System;
using Commands;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "slowdown before applying card")]
    public sealed class SlowdownSystem : BaseSystem, IReactGlobalCommand<CardAbilityHandledCommand>, IUpdatable
    {
        [Required] public SlowdownComponent SlowdownComponent;
        private TimeScaleComponent timeScaleComponent;
        public override void InitSystem()
        {
            AsSingle(ref timeScaleComponent);
        }

        public void CommandGlobalReact(CardAbilityHandledCommand command)
        {
            SlowdownComponent.RemainingTime = SlowdownComponent.Interval;
        }

        public void UpdateLocal()
        {
            timeScaleComponent.PrevScale = timeScaleComponent.Scale;
            SlowdownComponent.RemainingTime -= Time.deltaTime;
            if (SlowdownComponent.RemainingTime > 0)
            {
                timeScaleComponent.Scale = 1f;
            }
            else
            {
                timeScaleComponent.Scale = Mathf.MoveTowards(timeScaleComponent.Scale, SlowdownComponent.TimeScale,
                    SlowdownComponent.Speed * Time.deltaTime);
            }
        }
    }
}