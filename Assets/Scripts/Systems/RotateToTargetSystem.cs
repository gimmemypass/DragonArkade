using System;
using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class RotateToTargetSystem : BaseSystem, IUpdatable
    {
        [Required] public UnityTransformComponent UnityTransformComponent;
        [Required] public TargetEntityComponent TargetEntityComponent;
        [Required] public RotationComponent RotationComponent;
        public override void InitSystem()
        {
        }

        public void UpdateLocal()
        {
            if (TargetEntityComponent.Target == null)
                return;
            var transform = UnityTransformComponent.Transform;
            var dir = TargetEntityComponent.Target.GetComponent<UnityTransformComponent>().Transform.position -
                      transform.position;
            var needRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, needRot, RotationComponent.RotationSpeed * Time.deltaTime);
        }
    }
}