using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;
using Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.UI, "control ui for arena battle")]
    public sealed class ArenaBattleUIWindow : BaseSystem, IUpdatable, IHaveActor, IGlobalStart
    {
        private ArenaBattleHealthsMonoComponent monoComponent;
        public Actor Actor { get; set; }
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
        }

        public void GlobalStart()
        {
            var playerHealth = FactionHelper.GetHealthSum(FactionIdentifierMap.PlayerFactionIdentifier);
            monoComponent.PlayerHealth.SetMaxHealth(playerHealth);
            monoComponent.PlayerHealth.SetActualHealth(playerHealth);
        }

        public void UpdateLocal()
        {
            monoComponent.PlayerHealth.SetActualHealth(FactionHelper.GetHealthSum(FactionIdentifierMap.PlayerFactionIdentifier));
        }


    }
}