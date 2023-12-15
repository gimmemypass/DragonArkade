using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, "this strategy node set filter of entites, here we can setup include and exclude filters for entities filters")]
    public sealed class GetEntitiesFilterNode : GenericNode<EntitiesFilter>, IInitable
    {
        [DrawEntitiesFilter]
        public Filter Include;

        [DrawEntitiesFilter]
        public Filter Exclude;

        [ExposeField]
        public bool ForceUpdate;

        public override string TitleOfNode { get; } = "GetEntitiesFilterNode";

        [Connection(ConnectionPointType.Out, "Out <EntitiesFilter>")]
        public BaseDecisionNode BaseDecisionNode;

        public override void Execute(Entity entity)
        {
        }

        public override EntitiesFilter Value(Entity entity)
        {
            var filter = entity.World.GetFilter(Include, Exclude);

            if (ForceUpdate)
                filter.ForceUpdateFilter();
            
            return filter;
        }

        public void Init()
        {
        }
    }
}