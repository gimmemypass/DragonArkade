using System;
using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "system that rotate transform to target")]
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
            if (RotationComponent.OnlyY)
            {
                needRot = Quaternion.Euler(0,needRot.eulerAngles.y,0);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, needRot, RotationComponent.RotationSpeed * Time.deltaTime);
        }
    }
}