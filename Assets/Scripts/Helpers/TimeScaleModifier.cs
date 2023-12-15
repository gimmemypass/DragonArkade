using System;
using Components;
using HECSFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Helpers
{
    [Serializable]
    public class TimeScaleModifier : BaseModifier<float>
    {
        [SerializeField]
        private ModifierCalculationType calculationType = ModifierCalculationType.Multiply;

        private ModifierValueType modifierType = ModifierValueType.Value;

        [SerializeField, ReadOnly]
        private string guid;

        private Guid currentGuid;
        public override int ModifierID => ModifierIdentifierMap.TimeScale;

        public override float GetValue
        {
            get
            {
                var timeScaleComponent = EntityManager.Default.GetSingleComponent<TimeScaleComponent>();
                return timeScaleComponent.Scale;
            }
            set => throw new Exception("You cannot modify time scale modifier");
        }

        public override ModifierCalculationType GetCalculationType { get => calculationType; set => calculationType = value; }
        public override ModifierValueType GetModifierType { get => modifierType; set => modifierType = value; }

        public sealed override Guid ModifierGuid
        {
            get
            {
                if (currentGuid != Guid.Empty)
                    return currentGuid;

                if (string.IsNullOrEmpty(guid))
                {
                    currentGuid = Guid.NewGuid();
                    guid = currentGuid.ToString();
                }
                else
                {
                    currentGuid = new Guid(guid);
                    return currentGuid;
                }

                return currentGuid;
            }
            set
            {
                currentGuid = value;
            }
        }

    

        public TimeScaleModifier()
        {
            _ = ModifierGuid;
        }

        public override void Modify(ref float currentMod)
        {
            currentMod = ModifiersCalculation.GetResult(currentMod, GetValue, ModifierCalculationType.Multiply, GetModifierType);
        }
    }
}