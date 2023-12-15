using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, "Allow to run OneWayLogic")]
    public sealed class RunOneWayLogicNode : InterDecision, IInitable
    {
        public override string TitleOfNode { get; } = "RunOneWayLogicNode";

        [ExposeField]
        public OneWayLogic Logic;
        protected override void Run(Entity entity)
        {
            Logic.Execute(entity);      
        }

        public void Init()
        {
            if (Logic != null)
            {
                Logic.Exit = Next;
                Logic.Init();
            }
            else
            {
                HECSDebug.LogError("Logic field is empty");
            }
        }
    }
}