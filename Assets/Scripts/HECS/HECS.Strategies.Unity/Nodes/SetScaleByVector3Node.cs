using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "SetScaleByVector3Node")]
    public sealed class SetScaleByVector3Node : InterDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional entity")]
        public GenericNode<Entity> AdditionalEntity;

        [Connection(ConnectionPointType.In, "<Vector3> Scale")]
        public GenericNode<Vector3> Scale;

        public override string TitleOfNode { get; } = "SetScaleByVector3Node";

        protected override void Run(Entity entity)
        {
            var entityToScale = AdditionalEntity != null ? AdditionalEntity.Value(entity) : entity;

            if (entityToScale.TryGetComponent(out UnityTransformComponent unityTransformComponent))
                unityTransformComponent.Transform.localScale = Scale.Value(entity);

            Next.Execute(entity);
        }
    }
}
