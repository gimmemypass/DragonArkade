using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.Sphere, "")]
    public sealed class SpheresControllerTagComponent : BaseComponent, IWorldSingleComponent
    {
    }
}