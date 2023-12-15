using HECSFramework.Core;
using Strategies;

namespace HECSFramework.Unity.Strategies
{
    public sealed class IsEntityNotNullAndAlive : DilemmaDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional Entity")]
        public GenericNode<Entity> OptionalEntity;

        public override string TitleOfNode { get; } = "IsEntityNotNullAndAlive";

        protected override void Run(Entity entity)
        {
            var needed = OptionalEntity != null ? OptionalEntity.Value(entity) : entity;

            if (needed != null && needed.IsAlive())
            {
                Positive.Execute(entity);
                return;
            }

            Negative.Execute(entity);
        }
    }
}