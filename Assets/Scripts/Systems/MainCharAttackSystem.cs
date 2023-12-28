using System;
using Commands;
using Components;
using HECSFramework.Core;
using Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class MainCharAttackSystem : BaseSystem, IUpdatable
    {
        [Required] public TargetEntityComponent TargetEntityComponent;
        public override void InitSystem()
        {
        }

        public void UpdateLocal()
        {
             if (TargetEntityComponent.Target == null)
                 return;
             var dragonDefaultAttackAbility = EntityContainersMap.DragonDefaultAttackAbility;
             var isReady = ApplyAbilityIfReady(dragonDefaultAttackAbility);
        }

        private bool ApplyAbilityIfReady(int abilityId)
        {
            var isReady = AbilitiesHelper.CheckAbilityIsReady(Owner,
                abilityId,
                TargetEntityComponent.Target);
            if (isReady)
            {
                Owner.Command(new ExecuteAbilityByIDCommand
                {
                    AbilityIndex = abilityId,
                    Owner = Owner,
                    Target = TargetEntityComponent.Target,
                    Enable = true
                });
            }

            return isReady;
        }
    }
}