using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE,Doc.Visual,Doc.Rewards,Doc.Config, "visual reward config for soft currency")]
    public sealed class SoftCurrencyRewardVisualConfigComponent : BaseComponent
    {
        public CollectConfig CollectConfig;
    }
}