using System;
using Components;
using Components.MonoBehaviourComponents;
using HECSFramework.Core;
using HECSFramework.Unity;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.UI, "main character health ui")]
    public sealed class MainCharacterHealthUISystem : BaseSystem, IHaveActor, IGlobalStart, IUpdatable
    {
        public Actor Actor { get; set; }
        private HealthBarMonoComponent healthBarMonoComponent;
        private Entity mainCharacter;

        public override void InitSystem()
        {
            Actor.TryGetComponent(out healthBarMonoComponent, true);
        }

        public void GlobalStart()
        {
            mainCharacter = EntityManager.Default.GetSingleComponent<MainCharacterTagComponent>().Owner;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            var healthComponent = mainCharacter.GetComponent<HealthComponent>();
            healthBarMonoComponent.SetProgress(healthComponent.Value, healthComponent.CalculatedMaxValue);
        }

        public void UpdateLocal()
        {
            if (mainCharacter.TryGetComponent(out ShowHpBarTagComponent showHpBarTagComponent))
            {
                if(showHpBarTagComponent.IsUpdated) 
                    UpdateVisual();
                showHpBarTagComponent.IsUpdated = false;
            }   
        }
    }
}