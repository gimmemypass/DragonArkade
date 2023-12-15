using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, Doc.Visual, "Indicate that entity used in action")]
    public sealed class VisualInActionTagComponent : BaseComponent
    {
    }
}