using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.UniversalNodes, Doc.HECS, "this multiple two float values")]
    public sealed class FloatMultiplier : GenericNode<float>
    {
        public override string TitleOfNode { get; } = "FloatMultiplier";

        [Connection(ConnectionPointType.In, "<float> in A")]
        public GenericNode<float> ValueA;

        [Connection(ConnectionPointType.In, "<float> in B")]
        public GenericNode<float> ValueB;

        [Connection(ConnectionPointType.Out, "<float> A*B Result")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
        }

        public override float Value(Entity entity)
        {
            return ValueA.Value(entity) * ValueB.Value(entity);
        }
    }
}