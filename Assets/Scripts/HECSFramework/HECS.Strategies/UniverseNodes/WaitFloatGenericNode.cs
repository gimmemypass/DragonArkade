using System;
using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    public class WaitFloatGenericNode : InterDecision
    {
        public override string TitleOfNode { get; } = "WaitFloatGenericNode";

        [Connection(ConnectionPointType.In, "<float> Time")]
        public GenericNode<float> TimeForWait;

        protected override async void Run(Entity entity)
        {
            bool onPause = false;

            if (!entity.TryGetComponent(out StateContextComponent stateContextComponent))
                return;

            var currentIteration = stateContextComponent.CurrentIteration;
            
            if (stateContextComponent.StrategyState == StrategyState.Run)
            {
                onPause = true;
                stateContextComponent.StrategyState = StrategyState.Pause;
            }

            var alive = new AliveEntity(entity);
            var strategyIndex = stateContextComponent.CurrentStrategyIndex;

            await new Wait(TimeForWait.Value(entity)).RunJob(entity.World);

            if (!alive.IsAlive)
                return;

            if (strategyIndex != stateContextComponent.CurrentStrategyIndex)
                return;

            if (currentIteration != stateContextComponent.CurrentIteration)
                return;

            if (onPause)
                stateContextComponent.StrategyState = StrategyState.Run;

            Next.Execute(entity);
        }
    }
}