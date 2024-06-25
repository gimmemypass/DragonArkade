using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using Components;
using Components.MonoBehaviourComponents;
using DG.Tweening;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.UI, "energy ui system")]
    public sealed class EnergyUISystem : BaseSystem, IHaveActor, IUpdatable
    {
        public Actor Actor { get; set; }
        private EnergyUIMonoComponent monoComponent;
        private EnergyComponent energyComponent;
        private int prevValue;
        
        private Tween tween;

        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent, true);
            energyComponent = EntityManager.Default.GetSingleComponent<MainCharacterTagComponent>().Owner.GetComponent<EnergyComponent>();
            monoComponent.fillBar.fillAmount = 1;
            UpdateVisual();
        }

        public void UpdateLocal()
        {
            if (prevValue == (int)energyComponent.Value)
                return;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            prevValue = (int)energyComponent.Value;

            tween?.Kill();
            tween = monoComponent.fillBar.DOFillAmount((float)energyComponent.Value / energyComponent.CalculatedMaxValue,
                0.25f);
        }
    }
}