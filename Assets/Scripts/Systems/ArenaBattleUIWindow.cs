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
    [Documentation(Doc.NONE, "")]
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

            var enemyHealth = FactionHelper.GetHealthSum(FactionIdentifierMap.EnemyFactionIdentifier);
            monoComponent.EnemyHealth.SetMaxHealth(enemyHealth);
            monoComponent.EnemyHealth.SetActualHealth(enemyHealth);
        }

        public void UpdateLocal()
        {
            monoComponent.PlayerHealth.SetActualHealth(FactionHelper.GetHealthSum(FactionIdentifierMap.PlayerFactionIdentifier));
            monoComponent.EnemyHealth.SetActualHealth(FactionHelper.GetHealthSum(FactionIdentifierMap.EnemyFactionIdentifier));
        }


    }
}