using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class SpawnDropItemsComponent : BaseComponent
    {
        public float Radius;
        public float AngleStep;
        public float Cooldown;
    }
}