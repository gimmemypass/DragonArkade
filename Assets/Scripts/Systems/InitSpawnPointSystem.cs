using System;
using Components;
using Components.MonoBehaviourComponents;
using HECSFramework.Unity;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class InitSpawnPointSystem : BaseSystem, IHaveActor
    {
        [Required] public SpawnPointComponent SpawnPointComponent;
        public Actor Actor { get; set; }
        public override void InitSystem()
        {
            Actor.TryGetComponent(out SpawnPointMonoComponent monoComponent);
            SpawnPointComponent.SpawnPointIdentifier = monoComponent.SpawnPointIdentifier;
        }

    }
}