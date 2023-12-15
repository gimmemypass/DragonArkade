using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class TargetEntityComponent : BaseComponent
    {
        [NonSerialized]
        public Entity Target = null;
    }
}