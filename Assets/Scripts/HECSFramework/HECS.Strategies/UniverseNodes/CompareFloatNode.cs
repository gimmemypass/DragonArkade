using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.UniversalNodes, Doc.Strategy, "this node compare two float values A and B from generic nodes")]
    public sealed class CompareFloatNode : DilemmaDecision
    {
        [Connection(ConnectionPointType.In, "<float> ValueA")]
        public GenericNode<float> ValueA;

        [Connection(ConnectionPointType.In, "<float> ValueB")]
        public GenericNode<float> ValueB;

        [ExposeField]
        public Operations Operations;

        public override string TitleOfNode { get; } = "CompareFloatNode";

        protected override void Run(Entity entity)
        {
            switch (Operations)
            {
                case Operations.InEqual:
                    if (ValueA.Value(entity) == ValueB.Value(entity))
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
                case Operations.InMore:
                    if (ValueA.Value(entity) > ValueB.Value(entity))
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
                case Operations.InLess:
                    if (ValueA.Value(entity) < ValueB.Value(entity))
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
                case Operations.MoreOrEqual:
                    if (ValueA.Value(entity) >= ValueB.Value(entity))
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
                case Operations.LessOrEqual:
                    if (ValueA.Value(entity) <= ValueB.Value(entity))
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
            }
        }
    }
}