using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Components
{
    public abstract partial class ModifiableFloatCounterComponent : IAfterEntityInit
    {
        [SerializeField] private ModifierBluePrintBase[] baseModifiers;
        public void Recalc() => modifiableFloatCounter.Recalc();
        public void RecalcAndSetMax() => modifiableFloatCounter.RecalcAndSetMax();
        public void AfterEntityInit()
        {
            foreach (var baseModifier in baseModifiers)
            {
                AddModifier(Owner.GUID, (IModifier<float>)baseModifier.GetModifier());        
            }
        }
    }
}