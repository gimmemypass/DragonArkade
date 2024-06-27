using System;
using Commands;
using Components;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "default attack ability system")]
    public sealed class DefaultAttackAbilitySystem : BaseAbilitySystem
    {
        private CooldownComponent cooldownComponent;
        public override void InitSystem()
        {
            cooldownComponent = Owner.GetComponent<CooldownComponent>();
        }

        public override void Execute(Entity owner = null, Entity target = null, bool enable = true)
        {
            target!.Command(new DamageCommand<float>(){DamageDealer = owner, DamageValue = owner.GetComponent<DamageComponent>().Value});
            cooldownComponent.SetValue(cooldownComponent.CalculatedMaxValue);
        }
    }
}