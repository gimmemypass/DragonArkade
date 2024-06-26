using HECSFramework.Core;

namespace Strategies
{
    public interface IOneWayLogicExit
    {
        public BaseDecisionNode Exit { get; set; }
    }
    
    [Documentation(Doc.Strategy, "Final decision node for oneWayLogic")]
    public class OneWayLogicExitNode : FinalDecision, IOneWayLogicExit
    {
        public override string TitleOfNode { get; } = "OneWayLogicExit";
        public BaseDecisionNode Exit { get; set; }
        protected override void Run(Entity entity)
        {
            Exit.Execute(entity);
        }

    }
}