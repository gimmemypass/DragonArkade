using System;
using BluePrints.Identifiers;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class SpawnPointComponent : BaseComponent
    {
        public SpawnPointIdentifier SpawnPointIdentifier;
    }
}