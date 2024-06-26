using HECSFramework.Core;

namespace Strategies
{
    public class CheckGenericEntityAlive : GenericNode<Entity>
    {
        [Connection(ConnectionPointType.In, "<Entity>")]
        public GenericNode<Entity> InEntity;

        [Connection(ConnectionPointType.Out, "<CheckedEntity>")]
        public BaseDecisionNode CheckedEntity;

        [Connection(ConnectionPointType.In, "Input")]
        public BaseDecisionNode Input;

        [Connection(ConnectionPointType.Out, "Positive")]
        public BaseDecisionNode Positive;

        [Connection(ConnectionPointType.Out, "Negative")]
        public BaseDecisionNode Negative;

        public override string TitleOfNode { get; } = "CheckGenericEntityAlive";

        public override void Execute(Entity entity)
        {
            if (InEntity.Value(entity).IsAlive())
                Positive.Execute(entity);
            else
                Negative.Execute(entity);
        }

        public override Entity Value(Entity entity)
        {
            return InEntity.Value(entity);
        }
    }
}