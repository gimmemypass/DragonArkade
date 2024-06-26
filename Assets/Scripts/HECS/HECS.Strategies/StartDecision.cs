using HECSFramework.Core;

namespace Strategies
{
    public class StartDecision : BaseDecisionNode
    {
        [Connection(ConnectionPointType.Out, "Start of Strategy")] public BaseDecisionNode startDecision;

        public override string TitleOfNode => "Start";

        public override void Execute(Entity entity)
        {
            startDecision?.Execute(entity);
        }
    }
}