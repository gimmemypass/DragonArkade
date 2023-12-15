using System;
using HECSFramework.Core;

namespace Components
{
    [Serializable]
    [Documentation(Doc.Character, Doc.Stats, "Health Amount")]
    public sealed class HealthComponent : ModifiableFloatCounterComponent, IBaseValue<float>
    {
        [UnityEngine.SerializeField]
        private float healthBase = 20;

        public override int Id => CounterIdentifierContainerMap.Health;
        public override float SetupValue => healthBase;

        public float GetBaseValue => healthBase;

        public void SetupBaseValue(float newBaseValue)
        {
            modifiableFloatCounter.Setup(Id, newBaseValue);
        }
    }
}