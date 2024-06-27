using System;
using Commands;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Cards, "card to push sphere")]
    public sealed class CardPushSystem : BaseCardSystem
    {
        [Required] public PushDirectionComponent PushDirectionComponent;
        private Entity spheresController;

        public override void InitSystem()
        {
            base.InitSystem();
            spheresController = EntityManager.Default.GetSingleComponent<SpheresControllerTagComponent>().Owner;
        }

        protected override void Execute()
        {
            spheresController.GetOrAddComponent<PushDirectionComponent>().Direction = PushDirectionComponent.Direction;
            spheresController.Command(new ExecuteAbilityByIDCommand
            {
                AbilityIndex = EntityContainersMap.PushSphereAbility,
                Enable = true,
                Owner = Owner,
                Target = spheresController
            });
            spheresController.RemoveComponent<PushDirectionComponent>();
        }
        
        protected override bool CanExecute()
        {
            return AbilitiesHelper.CheckAbilityIsReady(spheresController, EntityContainersMap.AddSphereAbility,
                spheresController);
        }
    }
}