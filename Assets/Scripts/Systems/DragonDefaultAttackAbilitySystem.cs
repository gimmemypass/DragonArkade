using System;
using Components;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class DragonDefaultAttackAbilitySystem : BaseAbilitySystem
    {
        [Required] public CooldownComponent CooldownComponent;
        public override void InitSystem()
        {
        }

        public override void Execute(Entity owner = null, Entity target = null, bool enable = true)
        {
            HECSDebug.Log("Attack");            
            CooldownComponent.SetValue(CooldownComponent.CalculatedMaxValue);
        }
    }
}