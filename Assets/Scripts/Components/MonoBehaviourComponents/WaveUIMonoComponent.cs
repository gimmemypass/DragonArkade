using System;
using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    [RequireComponent(typeof(Image))]
    public class WaveUIMonoComponent : MonoBehaviour
    {
        private Image image;
        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void SetActiveWave(bool status)
        {
            image.color = status ? Active : Deactivated;
        }
        public Color Active;
        public Color Deactivated;
    }
}