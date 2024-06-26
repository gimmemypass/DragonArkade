using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    public sealed class ChangeFloatCounterNode : InterDecision
    {
        public override string TitleOfNode { get; } = "Change Float Counter";

        [DropDownIdentifier("CounterIdentifierContainer")]
        public int Counter;

        [SerializeField]
        public float Value;

        protected override void Run(Entity entity)
        {
            entity.GetComponent<CountersHolderComponent>()
                .GetCounter<ICounter<float>>(Counter).ChangeValue(Value);

            Next.Execute(entity);
        }
    }
}