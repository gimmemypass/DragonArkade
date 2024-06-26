using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, "GetFloatCounterNode")]
    public sealed partial class GetFloatCounterNode : GenericNode<float>
    {
        public override string TitleOfNode { get; } = "GetFloatCounterNode";

        [Connection(ConnectionPointType.In, " <Entity> Additional Entity")]
        public GenericNode<Entity> AdditionalEntity;

        [Connection(ConnectionPointType.Out, " <float> Out")]
        public BaseDecisionNode Out;

        [DropDownIdentifier("CounterIdentifierContainer")]
        public int CounterIdentifier;

        public override void Execute(Entity entity)
        {
        }

        public override float Value(Entity entity)
        {
            var neededEntity = AdditionalEntity != null ? AdditionalEntity.Value(entity) : entity;
            return neededEntity.GetComponent<CountersHolderComponent>().GetCounter<ICounter<float>>(CounterIdentifier).Value;
        }
    }
}
