using System;
using System.Collections.Generic;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class CollisionsComponent : BaseComponent
    {
        public LayerMask CollisionMask;
        public Dictionary<int, RaycastHit> EnterCollisions = new();
        public Dictionary<int, RaycastHit> StayCollisions = new();
    }
}