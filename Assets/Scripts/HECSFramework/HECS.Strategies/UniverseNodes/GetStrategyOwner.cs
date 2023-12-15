using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, "this node return entity that execute current strategy")]
    public class GetStrategyOwner : GenericNode<Entity>
    {
        public override string TitleOfNode { get; } = "GetStrategyOwner";

        [Connection(ConnectionPointType.Out, " <Entity> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
        }

        public override Entity Value(Entity entity)
        {
            return entity;
        }
    }
}
