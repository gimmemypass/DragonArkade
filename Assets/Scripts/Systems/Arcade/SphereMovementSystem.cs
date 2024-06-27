using System;
using Commands;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Helpers;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Sphere, "control sphere movement")]
    public sealed class SphereMovementSystem : BaseSystem, IUpdatable, IHaveActor, IGlobalStart
    {
        public Actor Actor { get; set; }

        private SpeedComponent speedComponent;
        private SphereComponent sphereComponent;
        private RigidbodyProviderComponent rbProvider;
        private CollisionsComponent collisionsComponent;
        
        
        private int buildingLayer;
        private ParticleSystem particleSystem;

        public override void InitSystem()
        {
            buildingLayer = LayerMask.NameToLayer("Building");
            sphereComponent = Owner.GetComponent<SphereComponent>();
            speedComponent = Owner.GetComponent<SpeedComponent>();
            rbProvider = Owner.GetComponent<RigidbodyProviderComponent>();
            collisionsComponent = Owner.GetComponent<CollisionsComponent>();
            Actor.TryGetComponent(out particleSystem, true);
        }

        public void GlobalStart()
        {
        }

        public void UpdateLocal()
        {
            UpdateDirection();
            foreach (var collision in collisionsComponent.EnterCollisions)
            {
                if (collision.Value.collider.TryGetActorFromCollision(out var actor) && actor != null)
                    Owner.Command(new CollideActorCommand { Actor = actor });
            }
            var position = rbProvider.Get.position;
            sphereComponent.XPosition = position.x;
            rbProvider.Get.velocity = sphereComponent.Dir.normalized * speedComponent.Value;
        }

        private void UpdateDirection()
        {
            Vector3 normal = Vector3.zero;
            foreach (var collision in collisionsComponent.EnterCollisions)
            {
                if (collision.Value.transform != null && collision.Value.transform.gameObject.layer == buildingLayer)
                {
                    normal = collision.Value.normal;
                    normal.x = 0;
                    normal.Normalize();
                    var newDir = Vector3.Reflect(sphereComponent.Dir, normal);
                    sphereComponent.Dir = newDir.normalized;
                }
            }

            if (normal != Vector3.zero)
            {
                particleSystem.Clear();     
                particleSystem.Play();
            }

            normal = Vector3.zero;
            foreach (var collision in collisionsComponent.StayCollisions)
            {
                if (collision.Value.transform != null && collision.Value.transform.gameObject.layer == buildingLayer)
                    normal += collision.Value.normal;
            }

            if (normal != Vector3.zero)
            {
                normal.x = 0;
                normal.Normalize();
                if (normal == Vector3.zero)
                    normal = new Vector3(0, 1, 0);
                sphereComponent.Dir = normal;
            }
        }

        public void FixedUpdateLocal()
        {
            
        }
    }
}