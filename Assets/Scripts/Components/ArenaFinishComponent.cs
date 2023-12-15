using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ArenaFinishComponent : BaseComponent
    {
        public int WinnerFaction;
    }
}