using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.Sphere, "")]
    public sealed class SphereComponent : BaseComponent
    {
        public Vector3 Dir;
        public float RadiusIncreaser = 1.01f;
        public float XPosition { get; set; }
        public ParticleSystem CollisionParticle;
    }
}