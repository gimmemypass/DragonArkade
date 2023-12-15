using System;
using Components;
using HECSFramework.Core;
using Strategies;
using UnityEngine;

namespace BluePrints.Strategies
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class IsOnPointNode : DilemmaDecision 
    {
        [Connection(ConnectionPointType.In, "Target point")]
        public GenericNode<Vector3> TargetPoint;

        [ExposeField]
        public float Accuracy;
        public override string TitleOfNode { get; } = "Is On Point";

        protected override void Run(Entity entity)
        {
            var sqrDist = (entity.GetComponent<UnityTransformComponent>().Transform.position - TargetPoint.Value(entity)).sqrMagnitude;
            if (sqrDist < Accuracy * Accuracy)
            {
                Positive.Execute(entity);
            }
            else
            {
                Negative.Execute(entity); 
            }
        }
    }
}