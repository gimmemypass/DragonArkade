using System;
using Commands;
using HECSFramework.Core;

namespace Systems
{
    [Serializable][Documentation(Doc.Death, Doc.Character, Doc.Battle, "we process here scenarios depended from death of character")]
    public sealed class DestroyAfterDeadEndAnimationEventSystem : BaseSystem, IReactCommand<AnimationEventCommand> 
    {
        public override void InitSystem()
        {
        }

        public void CommandReact(AnimationEventCommand command)
        {
            if (command.Id != AnimationEventIdentifierMap.DeathEnd)
                return;
            Owner.HecsDestroy();
        }
    }
}