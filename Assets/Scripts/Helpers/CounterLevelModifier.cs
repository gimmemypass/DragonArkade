using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using HECSFramework.Core;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace Helpers
{
    [Serializable]
    public class CounterLevelModifier : BaseModifier<float>
    {
        [Serializable]
        public struct DataPerLevel
        {
            public float Multiplier;
            public int Price;
        }
        [SerializeField] private List<DataPerLevel> multipliersPerLevel;
        [SerializeField] private bool useExtrapolation;
        [SerializeField] private float defaultLevelStep = 0.1f;
        [SerializeField] private int defaultPrice = 10;
        [SerializeField] private ModifierCalculationType defaultStepCalculationType = ModifierCalculationType.Add;
        [SerializeField] private ModifierCalculationType calculationType = ModifierCalculationType.Multiply;

        private ModifierValueType modifierType = ModifierValueType.Value;

        [SerializeField, ReadOnly] private string guid;

        private Guid currentGuid;
        private int modifierId;
        public override int ModifierID => modifierId;

        public void SetModifierId(int id) => modifierId = id;
        public override float GetValue
        {
            get
            {
                var playerUpgradeComponent = EntityManager.Default.GetSingleComponent<PlayerUpgradeComponent>();
                var level = playerUpgradeComponent.CountersUpgrades.GetValueOrDefault(modifierId, 0);
                if (multipliersPerLevel.Count > level)
                {
                    return multipliersPerLevel[level].Multiplier;
                }
                if(useExtrapolation)
                    return CalcByDefaultLevelStep(level);
                return multipliersPerLevel.Last().Multiplier;
            }
            set => throw new Exception("You cannot modify time scale modifier");
        }

        private float CalcByDefaultLevelStep(int level)
        {
            return defaultStepCalculationType switch
            {
                ModifierCalculationType.Add => CalcByDefaultAndAdding(defaultLevelStep, level),
                ModifierCalculationType.Subtract => CalcByDefaultAndAdding(-defaultLevelStep, level),
                ModifierCalculationType.Multiply => CalcByDefaultAndMultiply(defaultLevelStep, level),
                ModifierCalculationType.Divide => CalcByDefaultAndMultiply(1 / defaultLevelStep, level),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private float CalcByDefaultAndMultiply(float step, int level)
        {
            if (multipliersPerLevel.Count > 0)
            {
                return multipliersPerLevel[^1].Multiplier * Mathf.Pow(step, level - multipliersPerLevel.Count);
            }
            return Mathf.Pow(step, level);
        }
        private float CalcByDefaultAndAdding(float step, int level)
        {
            if (multipliersPerLevel.Count > 0)
                return multipliersPerLevel[^1].Multiplier + step * (level + 1 - multipliersPerLevel.Count);
            return 1 + step * level;
        }

        public int GetPrice
        {
            get
            {
                 var playerUpgradeComponent = EntityManager.Default.GetSingleComponent<PlayerUpgradeComponent>();
                 var level = playerUpgradeComponent.CountersUpgrades.GetValueOrDefault(modifierId, 0);
                 if (multipliersPerLevel.Count > level)
                 {
                     return multipliersPerLevel[level].Price;
                 }
                 if (multipliersPerLevel.Count > 0)
                     return multipliersPerLevel[^1].Price + defaultPrice * (level + 1 - multipliersPerLevel.Count);
                 return defaultPrice * level;               
            } 
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



        public CounterLevelModifier()
        {
            _ = ModifierGuid;
        }

        public override void Modify(ref float currentMod)
        {
            currentMod = ModifiersCalculation.GetResult(currentMod, GetValue, GetCalculationType, GetModifierType);
        }
    }
}