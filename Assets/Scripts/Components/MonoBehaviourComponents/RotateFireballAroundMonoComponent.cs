using System;
using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class RotateFireballAroundMonoComponent : MonoBehaviour
    {
        [SerializeField]
        private float radius;
        [SerializeField]
        private float speed;
        
        private Vector3 origin;
        private Camera mainCamera;
        private float angle;

        private void Awake()
        {
            mainCamera = Camera.main;
            origin = transform.position;
        }

        private void Update()
        {
            var mainCameraTransform = mainCamera.transform;
            var forward = mainCameraTransform.forward;
            var right = mainCameraTransform.right;
            var vector = right * radius;
            angle += speed * Time.deltaTime;
            angle %= 360;
            transform.position = Quaternion.AngleAxis(angle, forward) * vector + origin;
        }
    }
}