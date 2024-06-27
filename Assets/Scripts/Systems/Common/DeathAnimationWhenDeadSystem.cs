using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "death animation when dead system")]
    public sealed class DeathAnimationWhenDeadSystem : BaseSystem, IReactComponentLocal<IsDeadTagComponent>
    {
        public override void InitSystem()
        {
        }

        public void ComponentReact(IsDeadTagComponent component, bool isAdded)
        {
            if (!isAdded)
                return;
            Owner.Command(new TriggerAnimationCommand()
            {
                Index = AnimParametersMap.Death
            });
        }
    }
}