using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, Doc.UniversalNodes, "this node needed when we need split entities filter node result to two branches for reuse result without repeat calculation")]
    public sealed class SplitEntitiesFilterNode : SplitGenericNode<EntitiesFilter>
    {
        public override string TitleOfNode { get; } = "SplitEntitiesFilterNode";

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}