using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class SendLocalAnimationEventCommandsToAbilitiesSystem : BaseSystem, IReactCommand<AnimationEventCommand>
    {
        [Required] public AbilitiesHolderComponent AbilitiesHolderComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(AnimationEventCommand command)
        {
            foreach (var ability in AbilitiesHolderComponent.Abilities)
            {
                ability.Command(command); 
            }
        }
    }
}