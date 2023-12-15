using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.GameLogic, "")]
    public sealed class SpeedComponent : ModifiableFloatCounterComponent
    {
        [SerializeField]
        private float setupValue;
        public override int Id { get; } = CounterIdentifierContainerMap.Speed;
        public override float SetupValue => setupValue;
    }
}