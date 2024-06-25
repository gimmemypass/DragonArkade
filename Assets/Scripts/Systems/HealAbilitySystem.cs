using System;
using Commands;
using HECSFramework.Core;
using Components;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "heal ability system")]
    public sealed class HealAbilitySystem : BaseAbilitySystem
    {
        [Required] public HealthComponent HealthComponent;
        [Required] public HealAbilityComponent HealAbilityComponent;
        public override void InitSystem()
        {
        }

        public override void Execute(Entity owner = null, Entity target = null, bool enable = true)
        {
            Object.Instantiate(HealAbilityComponent.ParticleSystem);
            target.Command(new TriggerAnimationCommand(){Index = AnimParametersMap.Heal});
            target!.Command(new HealCommand()
            {
                Amount = HealthComponent.Value
            });
        }
    }
}