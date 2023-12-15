using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.HECS, Doc.Strategy, "this node return random entity from entities filter")]
    public class GetRandomEntityFromEntitiesFilter : GenericNode<Entity>
    {
        [Connection(ConnectionPointType.In, "<EntitiesFilter> In")]
        public GenericNode<EntitiesFilter> In;

        public override string TitleOfNode { get; } = "GetRandomEntityFromEntitiesFilter";

        [Connection(ConnectionPointType.Out, "<Entity> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public override Entity Value(Entity entity)
        {
            var filter = In.Value(entity);
            var random = UnityEngine.Random.Range(0, filter.Count);
            return filter[random];
        }
    }
}
