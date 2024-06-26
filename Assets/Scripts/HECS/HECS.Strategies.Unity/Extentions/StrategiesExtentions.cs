using HECSFramework.Core;

namespace Strategies
{
    public static class StrategiesExtentions
    {
        public static Entity Get(this GenericNode<Entity> genericNode, Entity entity)
        {
            return genericNode != null ? genericNode.Get(entity) : entity;
        }
    }
}
