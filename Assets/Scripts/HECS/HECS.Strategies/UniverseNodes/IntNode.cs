using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.HECS, Doc.UniversalNodes, "here we take int")]
    public class IntNode : GenericNode<int>
    {
        [ExposeField]
        public int ValueInt = 0;

        public override string TitleOfNode { get; } = "IntNode";

        [Connection(ConnectionPointType.Out, "<int> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public override int Value(Entity entity)
        {
            return ValueInt;
        }
    }
}
