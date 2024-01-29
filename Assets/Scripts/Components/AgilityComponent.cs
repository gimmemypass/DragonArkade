using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class AgilityComponent : ModifiableFloatCounterComponent
    {
        [SerializeField]
        private float baseValue;
        public override int Id { get; } = CounterIdentifierContainerMap.Agility;
        public override float SetupValue => baseValue;
        
        public void SetupBaseValue(float newBaseValue)
        {
            baseValue = newBaseValue;
            modifiableFloatCounter.Setup(Id, newBaseValue);
        }
    }
}