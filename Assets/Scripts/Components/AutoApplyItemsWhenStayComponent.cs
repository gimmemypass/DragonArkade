using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class AutoApplyItemsWhenStayComponent : BaseComponent
    {
        public float Cooldown = 0.5f;
        public float StartCooldown = 3f;
    }
}