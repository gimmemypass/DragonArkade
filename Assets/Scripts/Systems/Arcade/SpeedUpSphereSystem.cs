using System;
using Commands;
using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "System that speed up sphere")]
    public sealed class SpeedUpSphereSystem : BaseSystem, IReactGlobalCommand<CardAbilityHandledCommand>, IUpdatable, IAfterEntityInit
    {
        [Required] public SpeedUpSphereComponent SpeedUpSphereComponent;
        [Required] public SpeedComponent SpeedComponent;
        
        private float speedUpTime;
        private bool isSpeedUpped;
        
        private IModifier<float> modifier;

        public override void InitSystem()
        {
        }

        public void AfterEntityInit()
        {
            modifier = SpeedUpSphereComponent.ModifierBP.GetModifierWithCast<IModifier<float>>();
        }

        public void CommandGlobalReact(CardAbilityHandledCommand command)
        {
            if (!isSpeedUpped)
            {
                SpeedComponent.AddModifier(Owner.GUID, modifier);     
            }

            isSpeedUpped = true;
            speedUpTime = SpeedUpSphereComponent.SpeedUpTime;
        }

        public void UpdateLocal()
        {
            if (!isSpeedUpped)
                return;
            speedUpTime -= Time.deltaTime;
            if (speedUpTime < 0)
            {
                isSpeedUpped = false;
                SpeedComponent.RemoveModifier(Owner.GUID, modifier);
            }
        }
    }
}