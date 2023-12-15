using System;
using Commands;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class AutoApplyItemsWhenStaySystem : BaseSystem, IUpdatable
    {
        [Required] public AutoApplyItemsWhenStayComponent Component;
        [Required] public CharacterItemsComponent CharacterItemsComponent;
        private float cooldown;

        public override void InitSystem()
        {
            cooldown = Component.StartCooldown;
        }

        public void UpdateLocal()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                return;
            }

            if (CharacterItemsComponent.ItemInAim != null)
            {
                cooldown = Component.Cooldown;
                Owner.Command(new TryApplyAimedItemCommand());
            }
        }
    }
}