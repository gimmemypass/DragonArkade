using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.HECS, "here we check alive entity or not, and have it isdeadtag or not")]
    public sealed class IsEntityAliveAndNotDead : DilemmaDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional Entity")]
        public GenericNode<Entity> AdditionalNode;

        public override string TitleOfNode { get; } = "IsEntityAliveAndNotDead";

        protected override void Run(Entity entity)
        {
            var needed = AdditionalNode != null ? AdditionalNode.Value(entity) : entity;

            if (needed.IsAlive() && !needed.ContainsMask<IsDeadTagComponent>())
            {
                Positive.Execute(entity);
                return;
            }
            else
            {
                Negative.Execute(entity);
                return;
            }
        }
    }
}