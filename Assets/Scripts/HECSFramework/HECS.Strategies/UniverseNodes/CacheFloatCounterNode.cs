using Components;
using HECSFramework.Core;
using HECSFramework.Core.Helpers;
using HECSFramework.Unity;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Counters, Doc.Strategy, "This node save current value of counter to cache component, we can read it later, for compare or set this value again")]
    public sealed class CacheFloatCounterNode : InterDecision
    {
        public override string TitleOfNode { get; } = "CacheFloatCounterNode";

        [DropDownIdentifier("CounterIdentifierContainer")]
        public int CounterID;

        protected override void Run(Entity entity)
        {

            if (entity.TryGetComponent(out CountersHolderComponent countersHolderComponent))
            {
                if (countersHolderComponent.TryGetCounter<ICounter<float>>(CounterID, out var counter))
                {
                    var dic = entity.GetOrAddComponent<CacheCounterValuesComponent>().Values;
                    dic.AddOrReplace(CounterID, counter.Value);
                }
            }
            else
                throw new System.Exception("we dont have Counter Holder on Entity");

            Next.Execute(entity);
        }
    }
}