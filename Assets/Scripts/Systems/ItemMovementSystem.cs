using System;
using Components;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
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
            RigidbodyProviderComponent.Get.velocity = DirectionComponent.Direction * SpeedComponent.Value;
        }
    }
}