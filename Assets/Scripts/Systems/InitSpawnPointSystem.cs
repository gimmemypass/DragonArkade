using System;
using Components;
using Components.MonoBehaviourComponents;
using HECSFramework.Unity;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "init spawn point system")]
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