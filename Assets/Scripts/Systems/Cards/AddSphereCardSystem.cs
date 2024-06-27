using System;
using Commands;
using HECSFramework.Core;
using Components;
using Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Cards, "add sphere card system")]
    public sealed class AddSphereCardSystem : BaseCardSystem
    {
        private Entity spheresController;

        public override void InitSystem()
        {
            base.InitSystem();
            spheresController = EntityManager.Default.GetSingleComponent<SpheresControllerTagComponent>().Owner;
        }

        protected override void Execute()
        {
            spheresController.Command(new FloatAnimationCommand(){Index = AnimParametersMap.AppearBlend, Value = 1f});
            spheresController.Command(new ExecuteAbilityByIDCommand
            {
                AbilityIndex = EntityContainersMap.AddSphereAbility,
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