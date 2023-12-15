using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "RotateToTargetNode")]
    public sealed class RotateToTargetNode : InterDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Target")]
        public GenericNode<Entity> Target;
        [Connection(ConnectionPointType.In, "<float> Rotation Speed")]
        public GenericNode<float> RotationSpeed;
        [Connection(ConnectionPointType.In, "<Transform> Transform to rotation")]
        public GenericNode<Transform> TransformSource;

        [Connection(ConnectionPointType.Out, "<bool> IsRotatingComplete(by ConvertNode)")]
        public BaseDecisionNode IsRotatingOut;

        [ExposeField]
        public bool FixYAxis = true;

        public override string TitleOfNode { get; } = "RotateToTargetNode";

        protected override void Run(Entity entity)
        {
            var targetTransform = Target.Value(entity).GetComponent<UnityTransformComponent>().Transform;
            var entityTransform = TransformSource != null ? TransformSource.Value(entity) : entity.GetComponent<UnityTransformComponent>().Transform;
            var posTarget = targetTransform.position;
            var pos = entityTransform.position;

            if (FixYAxis)
            {
                posTarget.y = pos.y;
            }

            var dir = posTarget - pos;
            var quaternion = Quaternion.LookRotation(dir, Vector3.up);
            entityTransform.rotation = Quaternion.RotateTowards(entityTransform.rotation, quaternion, RotationSpeed.Value(entity)*Time.deltaTime);
            Next.Execute(entity);
        }

        [GetConvertNodeAttribute]
        public bool IsRotatingComplete(Entity entity)
        {
            var targetTransform = Target.Value(entity).GetComponent<UnityTransformComponent>().Transform;
            var entityTransform = TransformSource != null ? TransformSource.Value(entity) : entity.GetComponent<UnityTransformComponent>().Transform;

            var posTarget = targetTransform.position;
            var pos = entityTransform.position;

            var dir = posTarget - pos;
            var forward = entityTransform.forward;

            if (Vector3.Angle(forward, dir) < 0.05f)
            {
                return true;
            }
            else
                return false;
        }
    }
}