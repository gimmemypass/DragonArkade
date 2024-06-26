using Components;
using HECSFramework.Core;


namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, "IsStateNode")]
    public class IsStateNode : DilemmaDecision
    {
        public override string TitleOfNode { get; } = "Is State?";

        [DropDownIdentifier("GameStateIdentifier")]
        public int StateID;

        protected override void Run(Entity entity)
        {
            if (entity.World.GetSingleComponent<GameStateComponent>().CurrentState == StateID)
            {
                Positive.Execute(entity);
                return;
            }
            else
            {
                Negative.Execute(entity);
                return;
            }
        }
    }
}
