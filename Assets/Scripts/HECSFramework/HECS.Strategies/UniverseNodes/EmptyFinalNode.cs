using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.HECS, "Empty node whithout logic for finaling chains|strategis")]
    public sealed class EmptyFinalNode : BaseDecisionNode
    {
        [Connection(ConnectionPointType.In, "Input")]
        public BaseDecisionNode Input;

        public override string TitleOfNode { get; } = "EmptyFinalNode";

        public override void Execute(Entity entity)
        {
        }
    }
}