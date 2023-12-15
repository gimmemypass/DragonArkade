using System;
using Components.MonoBehaviourComponents;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class SoftCurrencyCounterMonoComponentProviderComponent : BaseProviderComponent<SoftValueUIMonoComponent>
    {
    }
}