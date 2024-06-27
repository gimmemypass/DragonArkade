using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, Doc.Sphere, "leveling sphere")]
    public sealed class SphereLevelingSystem : BaseSystem, IAfterEntityInit
    {
        [Required] public DamageComponent DamageComponent;
        private LevelModifierHolderComponent levelModifierHolderComponent;
        public override void InitSystem()
        {
            AsSingle(ref levelModifierHolderComponent);
        }

        public void AfterEntityInit()
        {
            DamageComponent.AddModifier(Owner.GUID,
                levelModifierHolderComponent.SphereDamage.GetModifierWithCast<IModifier<float>>());
        }
    }
}