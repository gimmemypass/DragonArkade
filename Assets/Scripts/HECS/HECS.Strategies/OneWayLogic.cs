using UnityEngine;

namespace Strategies
{
    [CreateAssetMenu(menuName = "Strategies/One Way Logic")]
    public sealed class OneWayLogic : BaseStrategy
    {
        public BaseDecisionNode Exit;

        public override void Init()
        {
            if (isInited)
                return;

            isInited = true;
            
            base.Init();
            
            foreach (var node in nodes)
            {
                if (node is IOneWayLogicExit oneWayLogicExit)
                {
                    oneWayLogicExit.Exit = Exit;
                } 
            }
        }
    }
}