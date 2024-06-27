using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using HECSFramework.Core;
using Components;
using HECSFramework.Unity;
using Helpers;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "system that try to apply aimed item")]
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

             CharacterItemsComponent.ItemInAim = ItemsHelper.GetAimedItem(CharacterItemsComponent.Items.Values, targetAngle, angle);
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
    }
}