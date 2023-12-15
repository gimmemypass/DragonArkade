using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class RotationComponent : BaseComponent
    {
        public float RotationSpeed = 1f;
    }
}