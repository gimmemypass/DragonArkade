using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using DG.Tweening;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Animation, "")]
    public sealed class ShakeAnimationWhenDamagedSystem : BaseSystem, IReactCommand<DamageForVisualFXCommand>, IHaveActor
    {
        [Required] public UnityTransformComponent TransformComponent;
        public Actor Actor { get; set; }
        public override void InitSystem()
        {
             
        }

        public void CommandReact(DamageForVisualFXCommand command)
        {
            TransformComponent.Transform.DOShakePosition(0.25f, 0.05f);
        }

    }
}