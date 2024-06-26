using System;
using System.Collections.Generic;
using HECSFramework.Core;
using Strategies;

namespace Components
{
    [Serializable, Documentation(Doc.Tag, Doc.AI, Doc.State, "Это компонент всю ключевую информацию про состояние персонажа")]
    public class StateContextComponent : BaseComponent, IDisposable
    {
        public StrategyState StrategyState = StrategyState.Stop;
        public State CurrentState;
        public IDecisionNode EarlyUpdateNode;
        public int CurrentStrategyIndex;
        public int CurrentIteration;
        
        public Stack<IDecisionNode> ExitStateNodes = new Stack<IDecisionNode>(3); //выход из текущего стейта, сеттим на входе в стейт

        public void ExitFromStates()
        {
            StrategyState = StrategyState.Stop;
            ExitStateNodes.Clear();
        }

        public void Dispose()
        {
            ExitFromStates();
            CurrentState = null;
            EarlyUpdateNode = null;
        }
    }
}