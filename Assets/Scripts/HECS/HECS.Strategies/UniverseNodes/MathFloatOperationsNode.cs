using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.UniversalNodes, Doc.Strategy, "this node provide basic math operations with A and B in generic float nodes")]
    public class MathFloatOperationsNode : GenericNode<float>
    {
        [Connection(ConnectionPointType.In, "<float> ValueA")]
        public GenericNode<float> ValueA;

        [Connection(ConnectionPointType.In, "<float> ValueB")]
        public GenericNode<float> ValueB;

        [ExposeField]
        public ModifierCalculationType CalculationType;

        [ExposeField]
        public ModifierValueType ValueType;

        [Connection(ConnectionPointType.Out, "<float> Out")]
        public BaseDecisionNode Out;

        public override string TitleOfNode { get; } = "MathFloatOperationsNode";

        public override void Execute(Entity entity)
        {
        }

        public override float Value(Entity entity)
        {
            return ModifiersCalculation.GetResult(ValueA.Value(entity), ValueB.Value(entity), CalculationType, ValueType);
        }
    }
}