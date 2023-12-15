using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class CooldownComponent : ModifiableFloatCounterComponent
    {
        [SerializeField]
        private float baseValue;
        public override int Id { get; } = CounterIdentifierContainerMap.Cooldown;
        public override float SetupValue => baseValue;
        
        public void SetupBaseValue(float newBaseValue)
        {
            baseValue = newBaseValue;
            modifiableFloatCounter.Setup(Id, newBaseValue);
        }
    }
    
}