using UnityEngine;

namespace Strategies
{
    public abstract class FinalDecision : LogNode
    {
        [HideInInspector, IgnoreDraw]
        [Connection(ConnectionPointType.In, "Input")] 
        public BaseDecisionNode node;
    }
}