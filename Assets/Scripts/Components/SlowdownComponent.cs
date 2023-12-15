using System;
using BluePrints;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class SlowdownComponent : BaseComponent
    {
        public float RemainingTime = 100f;
        public float Interval = 2f;
        
        [Range(0.1f,1f)]
        public float TimeScale = 0.25f;

        public float Speed = 1;
        public TimeScaleModifierBluePrint ModifierBluePrint;
    }
}