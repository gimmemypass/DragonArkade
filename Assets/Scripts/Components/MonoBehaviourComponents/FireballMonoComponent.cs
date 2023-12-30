using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    public class FireballMonoComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] trailVisuals;

        public void ActivateTrailVisual(bool status)
        {
            foreach (var trailVisual in trailVisuals)
            {
                trailVisual.gameObject.SetActive(status);
            } 
        }
    }
}