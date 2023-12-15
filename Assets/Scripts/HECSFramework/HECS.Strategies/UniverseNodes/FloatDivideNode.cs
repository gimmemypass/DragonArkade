using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.UniversalNodes, Doc.HECS, "this divide two float values")]
    public sealed class FloatDivideNode : GenericNode<float>
    {
        public override string TitleOfNode { get; } = "FloatDivideNode";

        [Connection(ConnectionPointType.In, "<float> in A")]
        public GenericNode<float> ValueA;

        [Connection(ConnectionPointType.In, "<float> in B")]
        public GenericNode<float> ValueB;

        [Connection(ConnectionPointType.Out, "<float> A/B Result")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
        }

        public override float Value(Entity entity)
        {
            var divider = ValueB.Value(entity);

            if (divider == 0)
            {
                HECSDebug.LogError($"we divide on zero {entity.ID} + {entity.GUID}");
                return 0;   
            }

            return ValueA.Value(entity) / divider;
        }
    }
}