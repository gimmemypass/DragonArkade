using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.HECS, Doc.UniversalNodes, "this node get first or default entity from filter, its alternative way to get single entity or first entity from possibilities")]
    public class GetFirstOrDefaultFromFilter : GenericNode<Entity>
    {
        public override string TitleOfNode { get; } = "GetFirstOrDefaultFromFilter";

        [Connection(ConnectionPointType.In, "<EntitiesFilter> In")]
        public GenericNode<EntitiesFilter> Filter;

        [Connection(ConnectionPointType.Out, "<Entity> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
        }

        public override Entity Value(Entity entity)
        {
            return Filter.Value(entity).FirstOrDefault();
        }
    }
}
