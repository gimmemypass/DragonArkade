using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "SetPositionNode")]
    public sealed class SetPositionNode : InterDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional entity")]
        public GenericNode<Entity> AdditionalEntity;

        [Connection(ConnectionPointType.In, "<Vector3> Position")]
        public GenericNode<Vector3> Position;

        public override string TitleOfNode { get; } = "SetPositionNode";

        protected override void Run(Entity entity)
        {
            var entityToRotation = AdditionalEntity != null ? AdditionalEntity.Value(entity) : entity;

            if (entityToRotation.TryGetComponent(out UnityTransformComponent unityTransformComponent))
                unityTransformComponent.Transform.position = Position.Value(entity);

            Next.Execute(entity);
        }
    }
}
