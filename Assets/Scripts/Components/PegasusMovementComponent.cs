using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class PegasusMovementComponent : BaseComponent
    {
        public float Speed = 1f;
        public float Amplitude = 1f;
    }
}