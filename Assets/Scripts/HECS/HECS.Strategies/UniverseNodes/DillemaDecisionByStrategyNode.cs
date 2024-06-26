using HECSFramework.Core;

namespace Strategies
{
    public class DillemaDecisionByStrategyNode : DilemmaDecision, IInitable
    {
        public override string TitleOfNode { get; } = "Decision by strategy";

        [ExposeField]
        public DilemmaStrategy Strategy;

        protected override void Run(Entity entity)
        {
            Strategy.Execute(entity);
        }

        public void Init()
        {
            if (Strategy != null)
            {
                Strategy.Positive = Positive;
                Strategy.Negative = Negative;
                
                Strategy.Init();
            }
            else
                HECSDebug.LogError("strategy field is empty");
        }
    }
}