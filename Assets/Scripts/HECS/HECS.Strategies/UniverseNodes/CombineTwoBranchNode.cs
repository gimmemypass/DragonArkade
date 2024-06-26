using HECSFramework.Core;

namespace Strategies
{
    public sealed class CombineTwoBranchNode : BaseDecisionNode
    {
        public override string TitleOfNode { get; } = "CombineTwoBranchNode";

        [Connection(ConnectionPointType.In, "A")]
        public BaseDecisionNode BranchA;

        [Connection(ConnectionPointType.In, "B")]
        public BaseDecisionNode BranchB;

        [Connection(ConnectionPointType.Out, "Exit")]
        public BaseDecisionNode Exit;

        public override void Execute(Entity entity)
        {
            Exit.Execute(entity);
        }
    }
}