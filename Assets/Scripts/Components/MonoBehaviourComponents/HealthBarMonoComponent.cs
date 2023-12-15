using System;
using System.Collections.Generic;
using System.Text;
using Helpers.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class HealthBarMonoComponent : MonoBehaviour
    {
        [SerializeField] private Image progressBarImage;
        [SerializeField] private Image delayDamageBarImage;
        [SerializeField] private TextMeshProUGUI currentHealthText;
        [SerializeField] private TextMeshProUGUI maxHealthText;

        private FillBar healthFillBar;
        private FillBar delayFillBar;

        private Dictionary<int, string> counterReductionMap = new()
        {
            { 3, "K" },
            { 6, "M" },
            { 9, "B" },
            { 12, "T" },
        };

        private StringBuilder stringBuilder = new();

        public void SetColor(Color color) =>
            progressBarImage.color = color;

        public void SetProgress(float current, float max)
        {
            if(currentHealthText != null)
                currentHealthText.text = CounterToString(current);
            if (maxHealthText != null)
                maxHealthText.text = CounterToString(max);
            

            float normalizedProgress = current / max;
            healthFillBar.SetBarSize(normalizedProgress);
            delayFillBar?.SetBarSize(normalizedProgress, delay: .5f); //TODO move values to config
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

        private void Awake()
        {
            healthFillBar = new FillBar(progressBarImage);
            healthFillBar.Init();

            if (delayDamageBarImage != null)
            {
                delayFillBar = new FillBar(delayDamageBarImage);
                delayFillBar.Init();
            }
        }

        private void OnDestroy()
        {
            healthFillBar?.CleanUp();
            delayFillBar?.CleanUp();
        }
    }
}