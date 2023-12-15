using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Random = UnityEngine.Random;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class PegasusMovementSystem : BaseSystem, IUpdatable
    {
        [Required] public RigidbodyProviderComponent RigidbodyProviderComponent;
        [Required] public PegasusMovementComponent PegasusMovementComponent;
        private float angle;

        public override void InitSystem()
        {
            PegasusMovementComponent.Speed *= Random.Range(0.7f, 1.3f);
        }

        public void UpdateLocal()
        {
            if (Owner.ContainsMask<IsDeadTagComponent>())
                return;
            var pos = Owner.GetComponent<UnityTransformComponent>().Transform.position - new Vector3(0, Mathf.Sin(2*angle), Mathf.Cos(angle + Mathf.PI/2f)) * PegasusMovementComponent.Amplitude;
            angle += Time.deltaTime * PegasusMovementComponent.Speed;
            RigidbodyProviderComponent.Get.MovePosition(pos + new Vector3(0, Mathf.Sin(2*angle), Mathf.Cos(angle + Mathf.PI/2f)) * PegasusMovementComponent.Amplitude);
        }
    }
}