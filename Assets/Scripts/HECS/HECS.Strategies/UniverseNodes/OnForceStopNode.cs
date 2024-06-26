using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, Doc.UniversalNodes, "this node call chain of nodes when this strategy have force stop")]
    public sealed class OnForceStopNode : BaseDecisionNode
    {
        public override string TitleOfNode { get; } = "OnForceStopNode";

        [Connection(ConnectionPointType.Out, "Stop")]
        public BaseDecisionNode Stop;

        public override void Execute(Entity entity)
        {
            Stop.Execute(entity);
        }
    }
}
