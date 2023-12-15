using System;
using Commands;
using HECSFramework.Core;
using Random = UnityEngine.Random;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class IterateAttackAnimationSystem : BaseSystem, IReactCommand<AnimationEventCommand>
    {

        public override void InitSystem()
        {
        }

        public void CommandReact(AnimationEventCommand command)
        {
            if (command.Id == AnimationEventIdentifierMap.StartAttack)
            {

            }
        }
    }
}