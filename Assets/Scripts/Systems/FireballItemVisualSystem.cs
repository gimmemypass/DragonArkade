using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "fireball item visual system")]
    public sealed class FireballItemVisualSystem : BaseSystem, IReactCommand<TryApplyItemCommand>, IHaveActor, IAfterEntityInit
    {
        private FireballMonoComponent monoComponent;
        public Actor Actor { get; set; }
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent, true);
        }

        public void AfterEntityInit()
        {
            monoComponent.ActivateTrailVisual(false); 
        }

        public void CommandReact(TryApplyItemCommand command)
        {
            monoComponent.ActivateTrailVisual(true); 
        }
    }
}