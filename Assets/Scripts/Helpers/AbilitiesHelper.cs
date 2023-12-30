using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Helpers
{
    public static class AbilitiesHelper
    {
        public static bool CheckAbilityIsReady(Entity abilityOwner, int abilityIndex, Entity target)
        {
            if (!abilityOwner.TryGetComponent(out AbilitiesHolderComponent abilitiesHolderComponent))
                return false;
            if (!abilitiesHolderComponent.IndexToAbility.TryGetValue(abilityIndex, out var ability))
                return false;
            if (!ability.TryGetComponent(out AbilityPredicateComponent predicatesComponent))
                return false;
            
            if (!predicatesComponent.TargetPredicates.IsReady(target, ability))
                return false;
            if (!predicatesComponent.AbilityPredicates.IsReady(ability))
                return false;
            if (!predicatesComponent.AbilityOwnerPredicates.IsReady(abilityOwner, target))
                return false;

            return true;
        } 
    }
}