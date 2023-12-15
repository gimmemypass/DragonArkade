using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, "GetEntityBySingleComponent")]
    public sealed class GetEntityBySingleComponent : GenericNode<Entity>
    {
        public override string TitleOfNode { get; } = "GetEntityBySingleComponent";

        [Connection(ConnectionPointType.Out, " <Entity> Out")]
        public BaseDecisionNode Out;

        [ComponentMaskDropDown]
        public int ComponentID;

        public override void Execute(Entity entity)
        {
        }

        public override Entity Value(Entity entity)
        {
            return entity.World.GetEntityBySingleComponent(ComponentID);
        }
    }
}
