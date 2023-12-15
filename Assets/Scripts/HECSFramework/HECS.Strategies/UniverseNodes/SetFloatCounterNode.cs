using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Strategies
{
    public sealed class SetFloatCounterNode : InterDecision
    {
        public override string TitleOfNode { get; } = "Set Float Counter";

        [DropDownIdentifier("CounterIdentifierContainer")]
        public int Counter;

        [SerializeField]
        public float Value;

        protected override void Run(Entity entity)
        {
            entity.GetComponent<CountersHolderComponent>()
                .GetCounter<ICounter<float>>(Counter).SetValue(Value);
            
            Next.Execute(entity);
        }
    }
}