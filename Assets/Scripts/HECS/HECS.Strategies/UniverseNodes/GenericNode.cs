using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, "this is base for providing value by other node")]
    public abstract class GenericNode<T> : BaseDecisionNode, IGenericNode<T>
    {
        public abstract T Value(Entity entity);
    }

    public abstract class ConvertNode<T, U>  : GenericNode<U>
    {
        [Connection(ConnectionPointType.In, "In")]
        public GenericNode<T> From;

        [Connection(ConnectionPointType.Out, "Out")]
        public BaseDecisionNode To; 
    }

    public interface IGenericNode<T>
    {
        abstract T Value(Entity entity);
    }
}