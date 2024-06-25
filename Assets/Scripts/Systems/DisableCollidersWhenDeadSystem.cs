using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using HECSFramework.Unity.Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "system for disable colliders when dead")]
    public sealed class DisableCollidersWhenDeadSystem : BaseSystem, IReactCommand<IsDeadCommand>, IHaveActor
    {
        [Required] public RigidbodyProviderComponent rigidbodyProviderComponent;
        private int ignoreSphereLayer;
        public Actor Actor { get; set; }
        public override void InitSystem()
        {
            ignoreSphereLayer = LayerMask.NameToLayer("IgnoreSphere");
        }

        public void CommandReact(IsDeadCommand command)
        {
            // Actor.TryGetComponents(out Collider[] colliders);
            // foreach (var collider in colliders)
            // {
            //     collider.enabled = false;
            // }
            Actor.gameObject.SetLayerRecursive(ignoreSphereLayer);
            rigidbodyProviderComponent.Get.useGravity = true;
        }

    }
}