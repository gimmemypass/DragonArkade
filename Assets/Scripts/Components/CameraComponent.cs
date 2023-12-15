using System;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.Camera, "Component with Camera")]
    public sealed class CameraComponent : BaseComponent, IHaveActor, IInitable
    {
        public Actor Actor { get; set; }
        public Camera Camera;

        public void Init()
        {
            Actor.TryGetComponent(out Camera, true);
        }
    }
}