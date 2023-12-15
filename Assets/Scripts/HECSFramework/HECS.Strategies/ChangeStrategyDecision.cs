using Commands;
using HECSFramework.Core;

namespace Strategies
{
    public class ChangeStrategyDecision : FinalDecision
    {
        public override string TitleOfNode { get; } = "Change Strategy";
        [UnityEngine.SerializeField] public Strategy Strategy;

        protected override void Run(Entity entity)
        {
            entity.Command(new ChangeStrategyCommand { Strategy = this.Strategy });
        }
    }
}