using System;
using Components;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, Doc.Item, "item movement system")]
    public sealed class ItemMovementSystem : BaseSystem, IUpdatable
    {
        [Required] public SpeedComponent SpeedComponent;
        [Required] public DirectionComponent DirectionComponent;
        [Required] public RigidbodyProviderComponent RigidbodyProviderComponent;
        public override void InitSystem()
        {
        }

        public void UpdateLocal()
        {
            if (RigidbodyProviderComponent.Get.isKinematic)
                return;
            RigidbodyProviderComponent.Get.velocity = DirectionComponent.Direction * SpeedComponent.Value;
        }
    }
}