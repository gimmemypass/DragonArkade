using System;
using Commands;
using Components;
using HECSFramework.Core;
using Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ChoosingPushDirCardSystem : BaseCardSystem
    {
        private Entity spheresController;

        public override void InitSystem()
        {
            base.InitSystem();
            spheresController = EntityManager.Default.GetSingleComponent<SpheresControllerTagComponent>().Owner;
        }

        protected override void Execute()
        {
            spheresController.Command(new ExecuteAbilityByIDCommand
            {
                AbilityIndex = EntityContainersMap.ChoosingPushDirectionSphereAbility,
                Owner = Owner,
                Target = spheresController,
                Enable = true
            });
        }
        protected override bool CanExecute()
        {
            return AbilitiesHelper.CheckAbilityIsReady(spheresController, EntityContainersMap.AddSphereAbility,
                spheresController);
        } 
    }
}