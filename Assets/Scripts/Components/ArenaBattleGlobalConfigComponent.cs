using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ArenaBattleGlobalConfigComponent : BaseComponent, IWorldSingleComponent
    {
        public int OpenAfterLevel = 3;
    }
}