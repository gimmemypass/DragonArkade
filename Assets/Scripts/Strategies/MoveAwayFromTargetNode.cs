using System;
using Components;
using HECSFramework.Core;
using Strategies;
using UnityEngine;

namespace BluePrints.Strategies
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class MoveAwayFromTargetNode : InterDecision
    {
        [Connection(ConnectionPointType.In, "MinDist")]
        public GenericNode<float> MinDist;

        [Connection(ConnectionPointType.In, "Speed")]
        public GenericNode<float> Speed;
        public override string TitleOfNode { get; } = "RB Move away from target Node";

        protected override void Run(Entity entity)
        {
            var target = entity.GetComponent<TargetEntityComponent>().Target;
            if (target != null)
            {
                var minDist = MinDist.Value(entity);
                var speed = Speed.Value(entity);
                var rb = entity.GetComponent<RigidbodyProviderComponent>().Get;
                var dir = entity.GetComponent<UnityTransformComponent>().Transform.position -
                          target.GetComponent<UnityTransformComponent>().Transform.position;
                dir.y = 0;
                if (dir.magnitude < minDist)
                {
                    rb.MovePosition(rb.position + dir.normalized * (speed * Time.deltaTime));
                }
            }

            Next.Execute(entity);
        }
    }
}