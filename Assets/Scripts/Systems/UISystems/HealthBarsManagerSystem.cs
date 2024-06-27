using System;
using System.Threading.Tasks;
using Commands;
using Components;
using Components.MonoBehaviourComponents;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.UI, Doc.Global, "This system reacts on healthbarTag component and response for call ui representation, plus movement ")]
    public sealed class HealthBarsManagerSystem : BaseSystem, 
        IReactComponentGlobal<ShowHpBarTagComponent>, IGlobalStart, ILateUpdatable, ILateStart
    {
        private UISystem uiSystem;
        private MainCameraComponent mainCameraComponent;
        private HealthBarsManagerComponent healthBarsManagerComponent;

        public void ComponentReactGlobal(ShowHpBarTagComponent component, bool isAdded)
        {
            if (component.Owner.ContainsMask<CustomHpBarComponent>())
                return;
            ProcessShowHpBarTagComponentAsync(component, isAdded).Forget();
        }

        private async UniTask ProcessShowHpBarTagComponentAsync(ShowHpBarTagComponent component, bool isAdded)
        {
            if (isAdded)
            {
                var healthBar = await uiSystem.ShowUI(UIIdentifierMap.HPBar_UIIdentifier,
                    true, AdditionalCanvasIdentifierMap.HealthBarsCanvas, true);

                component.HpBar = healthBar;
            }
            else
            {
                component.HpBar?.Command(new HideUICommand());
            }

            if (isAdded)
                healthBarsManagerComponent.ShowHpBarTags.Add(component);
            else
                healthBarsManagerComponent.ShowHpBarTags.Remove(component);
        }

        public override void InitSystem()
        {
            healthBarsManagerComponent = Owner.GetOrAddComponent<HealthBarsManagerComponent>();
        }

        public void LateStart()
        {
            uiSystem = Owner.World.GetSingleSystem<UISystem>();
        }

        public void GlobalStart()
        {
        }

        public void UpdateLateLocal()
        {
            foreach (var component in healthBarsManagerComponent.ShowHpBarTags)
            {
                if (!component.IsUpdated || !component.HpBar.IsAlive()) continue;
                var health = component.Owner.GetComponent<CountersHolderComponent>().GetCounter<ModifiableFloatCounterComponent>(CounterIdentifierContainerMap.Health);
                if (!component.IsAlive) continue;
                
                component.HpBar.Command(new RedrawBarCommand<float>
                {
                    CurrentValue = health.Value,
                    MaxValue = health.CalculatedMaxValue
                });
                component.IsUpdated = false;
            }

            foreach (var component in healthBarsManagerComponent.ShowHpBarTags)
            {
                if (!component.Owner.IsAlive() || component.Owner.IsDisposed)
                    continue;

                var healthBarPlaceMonoComponent = component.Owner.GetComponent<HealthBarPlaceMonoComponentProvider>().Get;
                var healthPlacePosition = healthBarPlaceMonoComponent.transform.position;
                var rect = component.HpBar.GetComponent<UnityRectTransformComponent>().RectTransform;
                
                mainCameraComponent ??= EntityManager.Default.GetSingleComponent<MainCameraComponent>();
                var pos = mainCameraComponent.Camera.WorldToScreenPoint(healthPlacePosition);
                pos.z = 0;
                rect.position = pos;
                rect.localScale = Vector3.one;
            }
        }
    }
}