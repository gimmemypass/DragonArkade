using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.UI, "")]
    public sealed class IconComponent : BaseComponent
    {
        public Sprite Icon;
    }
}