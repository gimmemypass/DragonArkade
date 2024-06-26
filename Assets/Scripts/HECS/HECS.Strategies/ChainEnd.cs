using System;
using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.AI, Doc.Strategy, "эта нода для стейтов, для стратегий должна использоваться нода Strategy Complete")]
    [Documentation(Doc.AI, Doc.Strategy, "Это нода пустышка которую мы ставим в конце цепочки для того чтобы не проверять на налл каждую следующую ноду, отличает от Complete ноды тем, что также содержит дебаг функционал для стака выполнения стейта")]
    public sealed class ChainEnd : LogNode, IAddStateNode 
    {
        [Connection(ConnectionPointType.In, "Input")]
        public BaseDecisionNode inputNode;

        [NonSerialized] private State state;

        public override string TitleOfNode => "Chain End";

        public void AddState(State state)
        {
            this.state = state;
        }

        protected override void Run(Entity entity)
        {
            var context = entity.GetComponent<StateContextComponent>();

            if (context.StrategyState == StrategyState.Stop)
                return;

            context.CurrentState = state;
            context.StrategyState = StrategyState.Run;
        }
    }
}