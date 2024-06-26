using HECSFramework.Core;
using HECSFramework.Unity;

namespace Strategies
{
    public class ExecuteActionNode : InterDecision
    {
        [ExposeField]
        public ActionBluePrint ActionBluePrint;
        public override string TitleOfNode { get; } = "ExecuteActionNode";

        protected override void Run(Entity entity)
        {
            ActionBluePrint.GetAction().Action(entity);
            Next.Execute(entity);
        }
    }
}