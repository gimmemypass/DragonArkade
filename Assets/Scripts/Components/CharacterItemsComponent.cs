using System;
using System.Collections.Generic;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class CharacterItemsComponent : BaseComponent
    {
        public float RotationSpeed = 1f;
        public float Radius = 1f;
        public float AimAngle = 15f;
        public Dictionary<int, Entity> Items = new();
        [NonSerialized]
        public Entity ItemInAim;
    }
}