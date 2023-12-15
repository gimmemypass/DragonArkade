using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Unity.VisualScripting;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class StopAIWhenDeadSystem : BaseSystem, IReactCommand<IsDeadCommand>
    {
        public override void InitSystem()
        {
        }

        public void CommandReact(IsDeadCommand command)
        {
            Owner.Command(new ForceStopAICommand());
        }
    }
}