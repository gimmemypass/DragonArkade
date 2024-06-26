using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "SetRotationAxisNode")]
    public sealed class SetRotationAxisNode : InterDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional entity")]
        public GenericNode<Entity> AdditionalEntity;

        [Connection(ConnectionPointType.In, "<Vector3> Axis")]
        public GenericNode<Vector3> Rotation;

        public override string TitleOfNode { get; } = "SetRotationAxisNode";

        protected override void Run(Entity entity)
        {
            var entityToRotation = AdditionalEntity != null ? AdditionalEntity.Value(entity) : entity;

            if (entityToRotation.TryGetComponent(out UnityTransformComponent unityTransformComponent))
                unityTransformComponent.Transform.rotation = Quaternion.Euler(Rotation.Value(entity));

           Next.Execute(entity);
        }
    }
}
