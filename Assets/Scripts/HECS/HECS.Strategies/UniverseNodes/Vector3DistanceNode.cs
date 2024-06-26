using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    public sealed class Vector3DistanceNode : DilemmaDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> A")]
        public GenericNode<Entity> A;

        [Connection(ConnectionPointType.In, "<Entity> B")]
        public GenericNode<Entity> B;

        [Connection(ConnectionPointType.In, "<float> Distance")]
        public GenericNode<float> Distance;

        [ExposeField]
        public bool Less = true;

        public override string TitleOfNode { get; } = "Vector3 DistanceNode";

        protected override void Run(Entity entity)
        {
            var posA = A.Value(entity).GetComponent<UnityTransformComponent>().Transform.position;
            var posB = B.Value(entity).GetComponent<UnityTransformComponent>().Transform.position;

            if (Vector3.Distance(posA, posB) < Distance.Value(entity))
            {
                if (Less)
                {
                    Positive.Execute(entity);
                    return;
                }
                else 
                {
                    Negative.Execute(entity);
                    return;
                }
            }

            if (!Less)
                Positive.Execute(entity);
            else
                Negative.Execute(entity);
        }
    }
}
