using System;
using Commands;
using HECSFramework.Core;
using Components;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "handle main char moving")]
    public sealed class MoveMainCharacterSystem : BaseSystem, IReactCommand<InputCommand>, IReactCommand<InputEndedCommand>, IUpdatable
    {
        [Required] public SpeedComponent SpeedComponent;
        [Required] public RigidbodyProviderComponent RigidbodyProviderComponent;

        public Vector2 input;
        public override void InitSystem()
        {
        }

        public void CommandReact(InputCommand command)
        {
            if (command.Index != InputIdentifierMap.Stick)
                return;
            input = command.Context.ReadValue<Vector2>();
            Owner.RemoveComponent<OnStayComponent>();
        }

        public void CommandReact(InputEndedCommand command)
        {
            if (command.Index != InputIdentifierMap.Stick)
                return;
            input = Vector2.zero;
            Owner.GetOrAddComponent<OnStayComponent>();
        }

        public void UpdateLocal()
        {
            if (Owner.ContainsMask<IsDeadTagComponent>())
                return;
            RigidbodyProviderComponent.Get.velocity = new Vector3(input.x, 0, input.y) * SpeedComponent.Value;
        }
    }
}