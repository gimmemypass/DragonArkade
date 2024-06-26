using HECSFramework.Core;

namespace Strategies
{
    public sealed class  CompareIntNode : DilemmaDecision
    {
        [Connection(ConnectionPointType.In, "<int>")]
        public GenericNode<int> IntValue;

        [ExposeField]
        public int Value;

        [ExposeField]
        public Operations Operations;

        public override string TitleOfNode { get; } = "CompareIntNode";

        protected override void Run(Entity entity)
        {
            switch (Operations)
            {
                case Operations.InEqual:
                    if (IntValue.Value(entity) == Value)
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
                case Operations.InMore:
                    if (IntValue.Value(entity) > Value)
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
                case Operations.InLess:
                    if (IntValue.Value(entity) < Value)
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
                case Operations.MoreOrEqual:
                    if (IntValue.Value(entity) >= Value)
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
                case Operations.LessOrEqual:
                    if (IntValue.Value(entity) <= Value)
                    {
                        Positive.Execute(entity);
                        return;
                    }
                    Negative.Execute(entity);
                    break;
            }
        }
    }

    public enum Operations { InEqual, InMore, InLess, MoreOrEqual, LessOrEqual }
}