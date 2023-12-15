using System;
using System.Collections.Generic;
using HECSFramework.Unity;
using HECSFramework.Core;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class UpgradeWindowComponent : BaseComponent
    {
        [SerializeField]
        private UpgradeData[] upgradeDatas;

        public IReadOnlyList<UpgradeData> UpgradeDatas => upgradeDatas;
    }

    [Serializable]
    public struct UpgradeData
    {
        public string Name;
        public CounterLevelModifierBluePrint ModifierBluePrint;
        public Sprite Icon;
    }
}