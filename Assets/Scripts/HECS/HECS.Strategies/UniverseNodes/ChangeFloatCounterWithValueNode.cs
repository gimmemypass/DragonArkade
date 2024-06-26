using Components;
using HECSFramework.Core;
using HECSFramework.Core.Helpers;

namespace Strategies
{
    public sealed class ChangeFloatCounterWithValueNode : InterDecision
    {
        public override string TitleOfNode { get; } = "Change Float Counter with value in";

        [DropDownIdentifier("CounterIdentifierContainer")]
        public int Counter;

        [Connection(ConnectionPointType.In, "<float> In")]
        public GenericNode<float> FloatIn;

        protected override void Run(Entity entity)
        {
            entity.GetComponent<CountersHolderComponent>()
                .GetCounter<ICounter<float>>(Counter).ChangeValue(FloatIn.Value(entity));

            Next.Execute(entity);
        }
    }
}