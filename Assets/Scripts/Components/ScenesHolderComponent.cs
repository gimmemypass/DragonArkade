using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ScenesHolderComponent : BaseComponent, IWorldSingleComponent
    {
        public string Battle = "Battle";
        public string Customization = "Customization";
        public string ArenaBattle = "ArenaBattle";
    }
}