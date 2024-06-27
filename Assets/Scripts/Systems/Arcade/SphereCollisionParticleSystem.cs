using System;
using System.Linq;
using Components;
using HECSFramework.Core;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "invoke particles when sphere colided")]
    public sealed class SphereCollisionParticleSystem : BaseSystem, IUpdatable
    {
        private CollisionsComponent collisionsComponent;
        private SphereComponent sphereComponent;
        private int buildingLayer;

        public override void InitSystem()
        {
            buildingLayer = LayerMask.NameToLayer("Building");
            collisionsComponent = Owner.GetComponent<CollisionsComponent>();
            sphereComponent = Owner.GetComponent<SphereComponent>();
        }

        public void UpdateLocal()
        {
            //todo pooling
            if (collisionsComponent.EnterCollisions.Count != 0)
            {
                var position = collisionsComponent.EnterCollisions.First().Value.point;
                Object.Instantiate(sphereComponent.CollisionParticle, position, Quaternion.identity);
            }
        }
    }
}