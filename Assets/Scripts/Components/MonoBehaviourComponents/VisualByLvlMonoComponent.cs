using System;
using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class VisualByLvlMonoComponent : MonoBehaviour
    {
        public ParticleSystem ParticleSystem;
        public GameObject[] AllElements;
        [Serializable]
        public struct Data
        {
            public GameObject[] Elements;
            public int lvl;
        }

        public Data[] Datas;
    }
}