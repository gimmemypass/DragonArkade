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
    public class AbilityOwnerDirectCorrelationCounterModifier : BaseModifier<float>
    {
        [SerializeField] private ModifierCalculationType calculationType = ModifierCalculationType.Multiply;
        [SerializeField] private CounterIdentifierContainer counterIdentifier;
        [SerializeField] private float multiplier;

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

        public Entity CounterOwner { get; set; }
        public override float GetValue
        {
            get
            {
                var abilityOwner = CounterOwner.GetComponent<AbilityOwnerComponent>();
                var countersHolderComponent = abilityOwner.AbilityOwner.GetComponent<CountersHolderComponent>();
                var counter = countersHolderComponent.GetCounter<ICounterModifiable<float>>(counterIdentifier.Id);
                return counter.GetForceCalculatedValue * multiplier;
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



        public AbilityOwnerDirectCorrelationCounterModifier()
        {
            _ = ModifierGuid;
        }

        public override void Modify(ref float currentMod)
        {
            currentMod = ModifiersCalculation.GetResult(currentMod, GetValue, GetCalculationType, GetModifierType);
        }
    }
}