using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.HECS, "this node provide count of entities in filter")]
    public sealed class GetEntitiesFilterCountNode : GenericNode<int>
    {
        public override string TitleOfNode { get; } = "GetEntitiesFilterCountNode";

        [Connection(ConnectionPointType.In, "<EntitiesFilter>")]
        public GenericNode<EntitiesFilter> Filter;

        [Connection(ConnectionPointType.Out, "<int>")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
        }

        public override int Value(Entity entity)
        {
            return Filter.Value(entity).Count;
        }
    }


}