using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "RotateToTargetNode")]
    public sealed class RotateInstantNode : InterDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Target")]
        public GenericNode<Entity> Target;

        [Connection(ConnectionPointType.In, "<Transform> TransformToRotate")]
        public GenericNode<Transform> TransformToRotate;

        [ExposeField]
        public bool FixYAxis = true;

        public override string TitleOfNode { get; } = "RotateInstantNode";

        protected override void Run(Entity entity)
        {
            var targetTransform = Target.Value(entity).GetComponent<UnityTransformComponent>().Transform;
            var entityTransform = TransformToRotate.Value(entity);
            var posTarget = targetTransform.position;
            var pos = entityTransform.position;

            if (FixYAxis)
            {
                posTarget.y = pos.y;
            }

            var dir = posTarget - pos;
            var quaternion = Quaternion.LookRotation(dir, Vector3.up);
            entityTransform.rotation = quaternion;
            Next.Execute(entity);
        }
    }
}
