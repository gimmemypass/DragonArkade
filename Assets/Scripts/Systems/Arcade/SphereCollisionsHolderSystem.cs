using System;
using System.Collections.Generic;
using Components;
using HECSFramework.Core;
using HECSFramework.Core.Helpers;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "custom holder for collisions")]
    public sealed class SphereCollisionsHolderSystem : BaseSystem, IPriorityUpdatable, IHaveActor
    {
        [Required] public UnityTransformComponent TransformComponent;
        [Required] public CollisionsComponent CollisionsComponent;
        private SphereComponent sphereComponent;
        public Actor Actor { get; set; }
        private RaycastHit[] raycastCache = new RaycastHit[32];
        private float radius;
        private Queue<int> stayCollisionRemoveQueue = new();

        public int Priority { get; } = -10;

        public override void InitSystem()
        {
            Actor.TryGetComponent(out SphereCollider sphereCollider);
            sphereComponent = Owner.GetComponent<SphereComponent>();
            radius = sphereCollider.radius * TransformComponent.Transform.lossyScale.Avg() * sphereComponent.RadiusIncreaser;
        }

        public void PriorityUpdateLocal()
        {
            var count = Physics.SphereCastNonAlloc(TransformComponent.Transform.position + new Vector3(-10, 0, 0),
                radius, TransformComponent.Transform.right, raycastCache, 20, CollisionsComponent.CollisionMask);
            
            //form stay collisions
            for (int i = 0; i < count; i++)
            {
                var instanceID = raycastCache[i].transform.gameObject.GetInstanceID();
                if(CollisionsComponent.EnterCollisions.ContainsKey(instanceID))
                {
                    CollisionsComponent.StayCollisions.AddOrReplace(instanceID, raycastCache[i]);
                }
            }
            foreach (var stayCollision in CollisionsComponent.StayCollisions)
            {
                var contains = false;
                for (int i = 0; i < count; i++)
                {
                    var raycastHit = raycastCache[i];
                    if (raycastHit.transform.gameObject.GetInstanceID() == stayCollision.Key)
                    {
                        contains = true;
                        break;
                    }
                }
                if(!contains || stayCollision.Value.transform == null) 
                    stayCollisionRemoveQueue.Enqueue(stayCollision.Key);
            }
            //remove
            while (stayCollisionRemoveQueue.Count > 0)
                CollisionsComponent.StayCollisions.Remove(stayCollisionRemoveQueue.Dequeue());
            for (int i = 0; i < count; i++)
            {
                var id = raycastCache[i].transform.gameObject.GetInstanceID();
                if(CollisionsComponent.StayCollisions.ContainsKey(id))
                    CollisionsComponent.StayCollisions[id] = raycastCache[i];
            }

            //form enter collisions
            CollisionsComponent.EnterCollisions.Clear();
            for (int i = 0; i < count; i++)
            {
                var id = raycastCache[i].transform.gameObject.GetInstanceID();
                if(CollisionsComponent.StayCollisions.ContainsKey(id))
                    continue;
                CollisionsComponent.EnterCollisions.AddOrReplace(id, raycastCache[i]);
            }
        }

    }
}