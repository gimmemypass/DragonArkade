using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ExplodeSpheresAbilityComponent : BaseComponent
    {
        public float Radius = 5f;
        public ParticleSystem ParticleSystem;
    }
}