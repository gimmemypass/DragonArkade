using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.UniversalNodes, Doc.HECS, Doc.Strategy, "this node provides filters for operations")]
    public sealed class FilterNode : GenericNode<FilterNode>
    {
        [DrawEntitiesFilter]
        public Filter Include;

        [DrawEntitiesFilter]
        public Filter Exclude;

        [Connection(ConnectionPointType.Out, "<FilterNode> Out")]
        public BaseDecisionNode Out;

        public override string TitleOfNode { get; } = "FilterNode";

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public override FilterNode Value(Entity entity)
        {
            return this;
        }
    }
}