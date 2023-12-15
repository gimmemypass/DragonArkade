using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class SpeedUpSphereComponent : BaseComponent
    {
        public FloatModifierBluePrint ModifierBP;
        public float SpeedUpTime = 0.5f;
    }
}