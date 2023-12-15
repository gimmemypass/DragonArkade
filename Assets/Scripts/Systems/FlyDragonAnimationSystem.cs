using System;
using Commands;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class FlyDragonAnimationSystem : BaseSystem, IUpdatable, IReactCommand<ItemAppliedCommand>, IReactCommand<DamageForVisualFXCommand>, IReactCommand<IsDeadCommand>
    {
        [Required] public UnityTransformComponent UnityTransformComponent;
        [Required] public RigidbodyProviderComponent RigidbodyProviderComponent;

        private Vector2 blend;
        private int attackAnimationIndex = 0;
        private const float AttackAnimationRange = 2;
        public override void InitSystem()
        {
        }

        public void UpdateLocal()
        {
            var rotation = UnityTransformComponent.Transform.rotation;
            var velocity = rotation * RigidbodyProviderComponent.Get.velocity;
            velocity.Normalize();
            blend = Vector2.MoveTowards(blend, new Vector2(velocity.x, velocity.z), 5*Time.deltaTime);
            
            Owner.Command(new FloatAnimationCommand()
            {
                Index = AnimParametersMap.HorizontalSpeed,
                Value = blend.x
            });
            Owner.Command(new FloatAnimationCommand()
            {
                Index = AnimParametersMap.VerticalSpeed,
                Value = blend.y 
            });
        }

        public void CommandReact(ItemAppliedCommand command)
        {
            Owner.Command(new IntAnimationCommand()
            {
                Index = AnimParametersMap.AttackBlend,
                Value = (int)(attackAnimationIndex++ % AttackAnimationRange)
            });
            
            Owner.Command(new TriggerAnimationCommand()
            {
                Index = AnimParametersMap.IsAttacking
            }); 
        }

        public void CommandReact(DamageForVisualFXCommand command)
        {
            Owner.Command(new TriggerAnimationCommand()
            {
                Index = AnimParametersMap.Hit
            }); 
        }

        public void CommandReact(IsDeadCommand command)
        {
             Owner.Command(new TriggerAnimationCommand()
             {
                 Index = AnimParametersMap.Death
             });
             RigidbodyProviderComponent.Get.useGravity = true;
             Owner.Command(new BoolAnimationCommand()
             {
                 Index = AnimParametersMap.Wings, 
                 Value = false
             });
        }
    }
}