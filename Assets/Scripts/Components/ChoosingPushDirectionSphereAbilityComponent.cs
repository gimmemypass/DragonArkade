using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ChoosingPushDirectionSphereAbilityComponent : BaseComponent
    {
        public FloatModifierBluePrint ResetToZeroModifier;
        public GameObject AimPrefab;
    }
}