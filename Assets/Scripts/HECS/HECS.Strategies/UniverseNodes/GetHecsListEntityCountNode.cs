using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.UniversalNodes, Doc.HECS, "this node provides count of hecs list with entities, we need it when we collecting entities for various processes")]
    public sealed class GetHecsListEntityCountNode : GenericNode<int> 
    {
        public override string TitleOfNode { get; } = "GetHecsListCountNode";

        [Connection(ConnectionPointType.In, "HECSList<Entity> In")]
        public GenericNode<HECSList<Entity>> In;

        [Connection(ConnectionPointType.Out, "<int> Count")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public override int Value(Entity entity)
        {
            return In.Value(entity).Count;
        }
    }
}
