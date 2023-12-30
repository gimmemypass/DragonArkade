using System;
using System.Linq;
using Commands;
using HECSFramework.Core;
using Components;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ApplyAimedItemSystem : BaseSystem, IReactCommand<TryApplyAimedItemCommand>, IUpdatable, IReactGlobalCommand<ItemAppliedCommand>
    {
        [Required] public CharacterItemsComponent CharacterItemsComponent;
        [Required] public AbilityOwnerComponent AbilityOwnerComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(TryApplyAimedItemCommand command)
        {
            var item = CharacterItemsComponent.ItemInAim;
            item?.Command(new TryApplyItemCommand());
        }
        public void CommandGlobalReact(ItemAppliedCommand command)
        {
            if (command.Owner.Index != AbilityOwnerComponent.AbilityOwner.Index)
                return;
            Owner.Command(new RemoveItemToCharacterCommand(){Item = command.Item});
        }

        public void UpdateLocal()
        {
             CharacterItemsComponent.ItemInAim = null;
             if (CharacterItemsComponent.Items.Count == 0)
                 return;

             var target = AbilityOwnerComponent.AbilityOwner.GetComponent<TargetEntityComponent>().Target;
             var dir = GetDirection(target);

             var targetAngle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
             var angle = CharacterItemsComponent.AimAngle;
             var item = CharacterItemsComponent.Items.Values
                 .Where(a => Mathf.Abs(NormalizeAngle(a.GetComponent<UnityTransformComponent>().Transform.rotation
                     .eulerAngles.y) - targetAngle) <= angle)
                 .OrderBy(a => Mathf.Abs(NormalizeAngle(a.GetComponent<UnityTransformComponent>().Transform.rotation.eulerAngles.y - targetAngle)))
                 .FirstOrDefault();
             CharacterItemsComponent.ItemInAim = item;
        }

        private Vector3 GetDirection(Entity target)
        {
            Vector3 dir;
            if (target == null)
            {
                dir = Vector3.forward;
            }
            else
                dir = target.GetComponent<UnityTransformComponent>().Transform.position -
                      AbilityOwnerComponent.AbilityOwner.GetComponent<UnityTransformComponent>().Transform.position;
            
            return dir;
        }

        private static float NormalizeAngle(float angle) //-180 to 180
        {
            //short way to get normalized between 0 and 360 degree
            angle = Quaternion.Euler(0, angle, 0).eulerAngles.y;
            
            if (angle > 180) angle -= 360;
            return angle;
        }
    }
}