using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, Doc.UniversalNodes, "this node needed when we need split generic entity node result to two branches for reuse result without repeat calculation")]
    public sealed class SplitEntityNode : SplitGenericNode<Entity>
    {
        public override string TitleOfNode { get; } = "SplitEntityNode";

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}