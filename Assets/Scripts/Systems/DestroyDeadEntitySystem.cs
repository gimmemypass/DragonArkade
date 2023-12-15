using System;
using Commands;
using Components;
using HECSFramework.Core;

namespace Systems
{
    [Serializable][Documentation(Doc.Death, Doc.Character, Doc.Battle, "we process here scenarios depended from death of character")]
    public sealed class DestroyDeadEntitySystem : BaseSystem, IReactCommand<IsDeadCommand> 
    {
        public void CommandReact(IsDeadCommand command)
        {
            Owner.HecsDestroy();
        }

        public override void InitSystem()
        {
        }
    }
}