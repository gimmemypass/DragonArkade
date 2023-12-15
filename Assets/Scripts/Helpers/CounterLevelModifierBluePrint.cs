using HECSFramework.Core;
using HECSFramework.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Helpers
{
    [CreateAssetMenu(menuName = "BluePrints/CounterLevelModifierBluePrint", fileName = "CounterLevelModifierBluePrint", order = 0)]
    public class CounterLevelModifierBluePrint : ModifierBluePrintBase 
    {
        [UnityEngine.SerializeField]
        [LabelText("Modifier")]
        private CounterLevelModifier modifier = new();
        
        public override IModifier GetModifier()
        {
            modifier.SetModifierId(name.GenerateIndex());
            return modifier;
        }

        public U GetModifierWithCast<U>() where U: IModifier
        {
            modifier.SetModifierId(name.GenerateIndex());
            if (modifier is U needed)
                return needed;

            return default;
        }
    }
}