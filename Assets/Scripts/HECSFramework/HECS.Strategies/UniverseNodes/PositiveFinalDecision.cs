using HECSFramework.Core;

namespace Strategies
{
    public class PositiveFinalDecision : FinalDecision, IPositiveDecision
    {
        public override string TitleOfNode { get; } = "PositiveFinalDecision";
        public BaseDecisionNode Positive { get; set; }

        protected override void Run(Entity entity)
        {
            Positive.Execute(entity);
        }
    }
}