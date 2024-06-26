using Commands;
using HECSFramework.Core;

namespace Strategies
{
    public class SetDefaultStrategyNode : FinalDecision
    {
        public override string TitleOfNode { get; } = "Set Default Strategy";

        protected override void Run(Entity entity)
        {
            entity.Command(new SetDefaultStrategyCommand());
        }
    }
}