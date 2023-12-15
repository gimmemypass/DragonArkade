using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class HealCardSystem : BaseCardSystem
    {
        private Entity mainCharacter;

        public override void InitSystem()
        {
            base.InitSystem();
            mainCharacter = EntityManager.Default.GetSingleComponent<MainCharacterTagComponent>().Owner;
        }

        protected override void Execute()
        {
            mainCharacter.Command(new ExecuteAbilityByIDCommand
            {
                AbilityIndex = EntityContainersMap.HealAbility,
                Owner = Owner,
                Target = mainCharacter
            });
        }
        protected override bool CanExecute()
        {
            return AbilitiesHelper.CheckAbilityIsReady(mainCharacter, EntityContainersMap.HealAbility,
                mainCharacter);
        }
    }
}