using Strategies;
using HECSFramework.Core;
using System;
using System.Net.Http.Headers;
using Components;
using UnityEngine;

namespace BluePrints.Strategies
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class SimpleMoveNode : InterDecision
    {
        [Connection(ConnectionPointType.In, "Target point")]
        public GenericNode<Vector3> TargetPoint;

        [Connection(ConnectionPointType.In, "Speed")]
        public GenericNode<float> Speed;
        public override string TitleOfNode { get; } = "Simple Move Node";

        protected override void Run(Entity entity)
        {
            var point = TargetPoint.Value(entity);
            var speed = Speed.Value(entity);
            var transform = entity.GetComponent<UnityTransformComponent>().Transform;
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
            Next.Execute(entity);
        }
    }
}