using Components;
using HECSFramework.Core;
using HECSFramework.Core.Helpers;
using UnityEngine;

namespace Strategies
{
    public sealed class SetFloatCounterWithValueNode : InterDecision
    {
        public override string TitleOfNode { get; } = "Set Float Counter with value in";

        [DropDownIdentifier("CounterIdentifierContainer")]
        public int Counter;

        [Connection(ConnectionPointType.In, "<float> In")]
        public GenericNode<float> FloatIn;

        protected override void Run(Entity entity)
        {
            entity.GetComponent<CountersHolderComponent>()
                .GetCounter<ICounter<float>>(Counter).SetValue(FloatIn.Value(entity));

            Next.Execute(entity);
        }
    }
}