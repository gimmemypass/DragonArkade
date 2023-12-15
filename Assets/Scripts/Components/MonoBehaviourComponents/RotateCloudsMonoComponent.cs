using System;
using Unity.Mathematics;
using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class RotateCloudsMonoComponent : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private void Update()
        {
            transform.rotation *= Quaternion.Euler(0,Time.deltaTime * speed, 0);
        }
    }
}