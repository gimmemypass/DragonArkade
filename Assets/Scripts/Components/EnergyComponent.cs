using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class EnergyComponent : ModifiableFloatCounterComponent
    {
        [SerializeField]
        private float baseValue;
        public override int Id => CounterIdentifierContainerMap.Energy;
        public override float SetupValue => baseValue;
    }
}