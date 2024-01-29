using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Helpers
{
    [Serializable]
    public class AbilityOwnerCounterModifier : BaseModifier<float>, IHaveOwner
    {
        [SerializeField] private ModifierCalculationType calculationType = ModifierCalculationType.Multiply;
        [SerializeField] private CounterIdentifierContainer counterIdentifier;
        [InfoBox("Example: \"1/x\" or sqrt(x) or x*x where x - value from needed counter")]
        [SerializeField] private string expression;

        private ModifierValueType modifierType = ModifierValueType.Value;

        [SerializeField, ReadOnly] private string guid;

        private Guid currentGuid;
        private int modifierId;
        public override int ModifierID
        {
            get => modifierId;
            set => modifierId = value;
        }

        public void SetModifierId(int id) => modifierId = id;

        public Entity Owner { get; set; }
        public override float GetValue
        {
            get
            {
                var abilityOwner = Owner.GetComponent<AbilityOwnerComponent>();
                var countersHolderComponent = abilityOwner.AbilityOwner.GetComponent<CountersHolderComponent>();
                var counter = countersHolderComponent.GetCounter<ICounterModifiable<float>>(counterIdentifier.Id);
                var counterValue = counter.GetForceCalculatedValue;
                var correctedExpression = expression.Replace("x", counterValue.ToString());
                if (ExpressionEvaluator.Evaluate(correctedExpression, out float value))
                {
                    return value;
                }
                throw new Exception("Something wrong with expression");
            }
            set => throw new Exception("You cannot modify the modifier");
        }

        public override ModifierCalculationType GetCalculationType
        {
            get => calculationType;
            set => calculationType = value;
        }

        public override ModifierValueType GetModifierType
        {
            get => modifierType;
            set => modifierType = value;
        }

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
            set { currentGuid = value; }
        }



        public AbilityOwnerCounterModifier()
        {
            _ = ModifierGuid;
        }

        public override void Modify(ref float currentMod)
        {
            currentMod = ModifiersCalculation.GetResult(currentMod, GetValue, GetCalculationType, GetModifierType);
        }

    }
}