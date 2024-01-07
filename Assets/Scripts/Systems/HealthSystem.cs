using System;
using Commands;
using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Character, Doc.Stats, "System for operating all around health")]
    public sealed class HealthSystem : BaseSystem, IReactCommand<DamageCommand<float>>,  IReactCommand<HealCommand>
    {
        [Required]
        public HealthComponent healthComponent;

        public override void InitSystem()
        {
        }

        public void CommandReact(DamageCommand<float> command)
        {
            if (Owner.ContainsMask<IsDeadTagComponent>() || Owner.ContainsMask<InvincibleComponent>())
                return;

            float dmg = command.DamageValue;
            healthComponent.ChangeValue(-dmg);

            Debug.Log($"прилетел дамаг {command.DamageValue} после резиста осталось {dmg}");

            var damageForVisualFXCommand = new DamageForVisualFXCommand()
            {
                Victim = Owner,
                StartDamage = command.DamageValue,
                DamageAfterResist = dmg,
                DamageType = command.DmgType
            };
            EntityManager.Command(damageForVisualFXCommand);
            Owner.Command(damageForVisualFXCommand);

            if (Owner.ContainsMask<NeedHpBarComponent>())
            {
                if (healthComponent.Value < healthComponent.CalculatedMaxValue)
                {
                    var showHPBar = Owner.GetOrAddComponent<ShowHpBarTagComponent>();
                    showHPBar.IsUpdated = true;
                }
                else
                    Owner.RemoveComponent<ShowHpBarTagComponent>();
            }

            if (healthComponent.Value <= 0)
            {
                Owner.GetOrAddComponent(new IsDeadTagComponent());
                Owner.Command(new IsDeadCommand());
            }
        }

        public void CommandReact(HealCommand command)
        {
            healthComponent.ChangeValue(command.Amount);
            var showHPBar = Owner.GetOrAddComponent<ShowHpBarTagComponent>();
            showHPBar.IsUpdated = true;
        }
    }
}