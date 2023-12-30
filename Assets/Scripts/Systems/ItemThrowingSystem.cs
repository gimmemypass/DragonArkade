using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ItemThrowingSystem : BaseSystem, IReactCommand<TryApplyItemCommand>
    {
        [Required] public RigidbodyProviderComponent RigidbodyProviderComponent;
        public override void InitSystem()
        {
            
        }

        public void CommandReact(TryApplyItemCommand command)
        {
            var itemOwner = Owner.GetComponent<BelongingComponent>().Entity;

            RigidbodyProviderComponent.Get.isKinematic = false;
            Owner.GetComponent<DirectionComponent>().Direction = GetDirection(itemOwner);
            EntityManager.Command(new ItemAppliedCommand(){Item = Owner, Owner = itemOwner});
        }
        
        private Vector3 GetDirection(Entity itemOwner)
        {
            Vector3 dir;
            var target = itemOwner.GetComponent<TargetEntityComponent>()?.Target;
            if (target == null)
            {
                dir = Vector3.forward;
            }
            else
                dir = target.GetComponent<UnityTransformComponent>().Transform.position -
                      Owner.GetComponent<UnityTransformComponent>().Transform.position;
            
            return dir.normalized;
        }
    }
}