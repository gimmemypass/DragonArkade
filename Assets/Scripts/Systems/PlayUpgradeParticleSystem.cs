using System;
using Commands;
using Components.MonoBehaviourComponents;
using HECSFramework.Core;
using HECSFramework.Unity;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, Doc.Visual, "play upgrade particle system")]
    public sealed class PlayUpgradeParticleSystem : BaseSystem, IReactGlobalCommand<LevelUpCommand>, IGlobalStart, IHaveActor
    {
        public Actor Actor { get; set; }
        private VisualByLvlMonoComponent monoComponent;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
        }

        public void GlobalStart()
        {
        }

        public void CommandGlobalReact(LevelUpCommand command)
        {
            monoComponent.ParticleSystem.Play();
        }
    }
}