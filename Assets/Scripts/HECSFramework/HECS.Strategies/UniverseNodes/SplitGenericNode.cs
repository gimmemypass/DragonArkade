using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.UniversalNodes, Doc.HECS, Doc.Strategy, "we should use this node when we need reuse generic result")]
    public abstract class SplitGenericNode<T> : GenericNode<T>
    {
        [Connection(ConnectionPointType.In, "In Generic")]
        public GenericNode<T> In;

        [Connection(ConnectionPointType.Out, "Out A")]
        public BaseDecisionNode A;

        [Connection(ConnectionPointType.Out, "Out B")]
        public BaseDecisionNode B;

        public override T Value(Entity entity)
        {
            return In.Value(entity);
        }
    }
}
