using System;
using System.Collections.Generic;
using System.Linq;
using HECSFramework.Unity;
using HECSFramework.Core;
using HECSFramework.Serialize;
using UnityEngine;

namespace Components
{
    [Serializable]
    [JSONHECSSerialize]
    [Documentation(Doc.NONE, "")]
    public sealed class PlayerUpgradeComponent : BaseComponent, IWorldSingleComponent, ISavebleComponent
    {
        [Field(0)]
        public Dictionary<int, int> CountersUpgrades = new();

        public int CurrentLevel => CountersUpgrades.Values.Sum();
    }
}