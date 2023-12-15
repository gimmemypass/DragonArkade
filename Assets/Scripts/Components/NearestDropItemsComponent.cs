using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class NearestDropItemsComponent : BaseComponent
    {
        // public int DropItemInFocus; // container index
        public Entity DropItem;
    }
}