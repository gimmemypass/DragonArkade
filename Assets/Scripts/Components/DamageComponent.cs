using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.Damage, "")]
    public sealed class DamageComponent : ModifiableFloatCounterComponent
    {
        [SerializeField]
        private float baseValue;
        public override int Id => CounterIdentifierContainerMap.Damage;
        public override float SetupValue => baseValue;
    }
}