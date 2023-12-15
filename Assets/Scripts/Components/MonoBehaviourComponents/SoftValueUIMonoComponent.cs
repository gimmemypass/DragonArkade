using TMPro;
using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class SoftValueUIMonoComponent : MonoBehaviour
    {
        public RectTransform CoinsPoint;
        
        [SerializeField] private TextMeshProUGUI SoftCurrencyText;

        public void SetCounterValue(string value)
        {
            SoftCurrencyText.text = $"{value}";
        }

        public void SetCounterValue(int value)
        {
            SoftCurrencyText.text = $"{value}";
        }
    }
}