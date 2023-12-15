using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using Helpers;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class LevelModifierHolderComponent : BaseComponent, IWorldSingleComponent
    {
        public CounterLevelModifierBluePrint SphereDamage;
        public CounterLevelModifierBluePrint MainCharHealth;
        public CounterLevelModifierBluePrint MainCharEnergy;
    }
}