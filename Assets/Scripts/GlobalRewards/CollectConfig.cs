using System;
using UnityEngine;

namespace Components
{
    [CreateAssetMenu(menuName = "Effects/Collect", fileName = "collect_anim_config")]
    public class CollectConfig : ScriptableObject
    {
        [Serializable]
        public class MinMaxValue
        {
            public MinMaxValue(float minValue = 0, float maxValue = 0)
            {
                m_minValue = minValue;
                m_maxValue = maxValue;
            }

            public float GetRandomBetween() => 
                UnityEngine.Random.Range(m_minValue, m_maxValue);

            public float MinValue => m_minValue;
            public float MaxValue => m_maxValue;

            [SerializeField] private float m_minValue = 0;
            [SerializeField] private float m_maxValue = 0;
        }

        public MinMaxValue DelayTime => _delayTime;
        public MinMaxValue DelayStartTime => _delayStartTime;

        public MinMaxValue PosPerp => _posPerp;
        public MinMaxValue LenghtPerp => _lenghtPerp;

        public AnimationCurve ScaleProgress => _scaleProgress;
        public AnimationCurve ProgressCurve => _progressCurve;


        [Space] [Header("Time")] 
        [SerializeField] private MinMaxValue _delayTime = new MinMaxValue(0.9f, 1.4f);
        [SerializeField] private MinMaxValue _delayStartTime = new MinMaxValue(0f, 0.3f);

        [Space] [Header("Perpendicular")] 
        [SerializeField] private MinMaxValue _posPerp = new MinMaxValue(0.25f, 0.75f);
        [SerializeField] private MinMaxValue _lenghtPerp = new MinMaxValue(0f, 0.2f);

        [Space] [Header("Progress state")]
        [SerializeField] private AnimationCurve _scaleProgress = null;
        [SerializeField] private AnimationCurve _progressCurve = null;
        
        public bool RelativeOneScale = true;

        [field:Header("Other")] 
        [field: SerializeField] public bool CopySizeOfEndObject { get; private set; }
    }
}
