using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class ArenaBattleCharacterHealthMonoComponent : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI currentHealthText;
        [SerializeField] private TextMeshProUGUI maxHealthText;
        
        
        private readonly StringBuilder stringBuilder = new();
        private readonly Dictionary<int, string> counterReductionMap = new()
        {
            { 3, "K" },
            { 6, "M" },
            { 9, "B" },
            { 12, "T" },
        };
        
        private int maxHealth;
        public void SetMaxHealth(int health)
        {
            maxHealth = health;
            maxHealthText.text = CounterToString(health);
        }

        public void SetActualHealth(int health)
        {
            currentHealthText.text = CounterToString(health);
            slider.value = (float)health / maxHealth;
        }
        
        private string CounterToString(float current)
        {
            //todo string allocations
            var counterToString = current.ToString();
            var length = counterToString.Length - 1;
            var key = length - length % 3;
            if (counterReductionMap.TryGetValue(key, out var reduction))
            {
                stringBuilder.Clear();
                stringBuilder.Append((current / Mathf.Pow(10, key)).ToString("0.0"));
                stringBuilder.Append(reduction);
                return stringBuilder.ToString();
            }

            return counterToString;
        }
    }
}