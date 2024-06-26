using HECSFramework.Core;
using HECSFramework.Unity.Helpers;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.UniversalNodes, Doc.HECS, "this node return random element from hecs list with entities")]
    public sealed class GetRandomFromEntitiesListNode : GenericNode<Entity>
    {
        public override string TitleOfNode { get; } = "GetRandomFromEntitiesListNode";

        [Connection(ConnectionPointType.In, "HECSList<Entity> In")]
        public GenericNode<HECSList<Entity>> In;

        [Connection(ConnectionPointType.Out, "<Entity> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public override Entity Value(Entity entity)
        {
            return In.Value(entity).GetRandomElement();
        }
    }
}
