using System;
using Components;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "system that control sphere speed depend on timescale")]
    public sealed class DependOnTimeScaleSpeedSystem : BaseSystem, IUpdatable, IGlobalStart, IReactComponentGlobal<SpeedComponent>
    {
        private SlowdownComponent slowdownComponent;
        private TimeScaleComponent timeScaleComponent;
        private EntitiesFilter speedComponentFilter;
        private IModifier<float> modifier;

        public override void InitSystem()
        {
            slowdownComponent = Owner.GetComponent<SlowdownComponent>();
            speedComponentFilter = EntityManager.Default.GetFilter(Filter.Get<SpeedComponent, SpeedDependsOnTimeScaleComponent>());
            modifier = slowdownComponent.ModifierBluePrint.GetModifierWithCast<IModifier<float>>();
        }

        public void GlobalStart()
        {
            AsSingle(ref timeScaleComponent);
        }

        public void UpdateLocal()
        {
            foreach (var ent in speedComponentFilter)
            {
                var speedComponent = ent.GetComponent<SpeedComponent>();
                speedComponent.RecalcAndSetMax();
            }
        }

        public void ComponentReactGlobal(SpeedComponent component, bool isAdded)
        {
            if(isAdded && component.Owner.ContainsMask<SpeedDependsOnTimeScaleComponent>())
                component.AddUniqueModifier(Owner.GUID, modifier);
        }
    }
}