using System;
using Commands;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Unity.VisualScripting;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class AutoApplyItemsWhenStaySystem : BaseAbilitySystem, IUpdatable
    {
        [Required] public CooldownComponent CooldownComponent;
        [Required] public CharacterItemsComponent CharacterItemsComponent;

        public override void InitSystem()
        {
        }

        public void UpdateLocal()
        {
            if (CooldownComponent.Value > 0)
            {
                return;
            }

            if (CharacterItemsComponent.ItemInAim != null)
            {
                CooldownComponent.SetValue(CooldownComponent.CalculatedMaxValue);
                Owner.Command(new TryApplyAimedItemCommand());
            }
        }

        public override void Execute(Entity owner = null, Entity target = null, bool enable = true)
        {
            CooldownComponent.SetValue(CooldownComponent.CalculatedMaxValue);
        }
    }
}