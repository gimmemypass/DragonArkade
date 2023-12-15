using System;
using Components;
using HECSFramework.Core;
using Strategies;
using UnityEngine;

namespace BluePrints.Strategies
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class RigidbodyMoveNode : InterDecision
    {
        [Connection(ConnectionPointType.In, "Target point")]
        public GenericNode<Vector3> TargetPoint;

        [Connection(ConnectionPointType.In, "Speed")]
        public GenericNode<float> Speed;
        public override string TitleOfNode { get; } = "Rigidbody Move Node";

        protected override void Run(Entity entity)
        {
            var point = TargetPoint.Value(entity);
            var speed = Speed.Value(entity);
            var rb = entity.GetComponent<RigidbodyProviderComponent>().Get;
            // rb.MovePosition(Vector3.MoveTowards(rb.position, point, speed * Time.deltaTime));
            rb.velocity = (point - rb.position).normalized * speed;
            Next.Execute(entity);
        }
    }
}