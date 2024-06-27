using System;
using Commands;
using Components;
using HECSFramework.Core;
using Random = UnityEngine.Random;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "random attack animations")]
    public sealed class RandomAttackAnimationSystem : BaseSystem, IReactCommand<AnimationEventCommand>
    {
        private const float AttackAnimationRange = 2;
        public override void InitSystem()
        {
        }

        public void CommandReact(AnimationEventCommand command)
        {
            if (command.Id == AnimationEventIdentifierMap.StartAttack)
            {
                Owner.Command(new FloatAnimationCommand()
                {
                    Index = AnimParametersMap.AttackBlend,
                    Value = (int)(Random.value * AttackAnimationRange)
                });
            }
        }
    }
}