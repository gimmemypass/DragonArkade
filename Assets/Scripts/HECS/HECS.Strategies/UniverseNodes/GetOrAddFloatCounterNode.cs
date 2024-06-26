using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, "this node try to get needed float counter, if we dont have it - add simple float counter")]
    public class GetOrAddFloatCounterNode : InterDecision
    {
        public override string TitleOfNode { get; } = "GetOrAddFloatCounterNode";

        [DropDownIdentifier("CounterIdentifierContainer")]
        public int CounterID;


        protected override void Run(Entity entity)
        {
            var holder = entity.GetComponent<CountersHolderComponent>();

            if (!holder.TryGetCounter(CounterID, out ICounter _))
            {
                holder.AddCounter(new DefaultFloatCounter { Id = CounterID });
            }

            Next.Execute(entity);
        }
    }
}
