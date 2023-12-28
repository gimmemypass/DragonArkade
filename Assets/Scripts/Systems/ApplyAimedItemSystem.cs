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
    public sealed class ApplyAimedItemSystem : BaseSystem, IReactCommand<TryApplyAimedItemCommand>, IHaveActor, IUpdatable, IReactCommand<ItemAppliedCommand>
    {
        public Actor Actor { get; set; }
        [Required] public CharacterItemsComponent CharacterItemsComponent;
        [Required] public TargetEntityComponent TargetEntityComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(TryApplyAimedItemCommand command)
        {
            var item = CharacterItemsComponent.ItemInAim;
            if (item == null)
                return;
            item.Command(new TryApplyItemCommand());
        }
        
        public void CommandReact(ItemAppliedCommand command)
        {
            Owner.Command(new RemoveItemToCharacterCommand(){Item = command.Item});
        }

        public void UpdateLocal()
        {
             CharacterItemsComponent.ItemInAim = null;
             if (CharacterItemsComponent.Items.Count == 0)
                 return;
 
             var dir = GetDirection();

             var targetAngle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
             var angle = CharacterItemsComponent.AimAngle;
             var item = CharacterItemsComponent.Items.Values
                 .Where(a => Mathf.Abs(NormalizeAngle(a.GetComponent<UnityTransformComponent>().Transform.rotation
                     .eulerAngles.y) - targetAngle) <= angle)
                 .OrderBy(a => Mathf.Abs(NormalizeAngle(a.GetComponent<UnityTransformComponent>().Transform.rotation.eulerAngles.y - targetAngle)))
                 .FirstOrDefault();
             CharacterItemsComponent.ItemInAim = item;
        }

        private Vector3 GetDirection()
        {
            Vector3 dir;
            if (TargetEntityComponent.Target == null)
            {
                dir = Vector3.forward;
            }
            else
                dir = TargetEntityComponent.Target.GetComponent<UnityTransformComponent>().Transform.position -
                      Owner.GetComponent<UnityTransformComponent>().Transform.position;
            
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