using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class EnergyRegenerationComponent : ModifiableIntCounterComponent
    {
        [SerializeField]
        private int baseValue;
        public override int Id => CounterIdentifierContainerMap.EnergyRegen;
        public override int SetupValue => baseValue;
    }
}