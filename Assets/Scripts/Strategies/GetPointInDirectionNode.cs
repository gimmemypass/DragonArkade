using System;
using Components;
using HECSFramework.Core;
using Strategies;
using UnityEngine;

namespace HECSFramework.Unity.Strategies
{
    [Serializable]
    public class GetPointInDirectionNode : GenericNode<Vector3>
    {
        public override string TitleOfNode { get; } = "Get Point in direction Node";

        [Connection(ConnectionPointType.In, "<float> Speed")]
        public GenericNode<float> Speed;
        [Connection(ConnectionPointType.In, "<float> Direction")]
        public GenericNode<Vector3> Direction;
        
        [Connection(ConnectionPointType.Out, " <Vector3> Out")]
        public BaseDecisionNode Out;
        public override Vector3 Value(Entity entity)
        {
            return entity.GetComponent<UnityTransformComponent>().Transform.position + Direction.Value(entity) * Speed.Value(entity);
        }

        public override void Execute(Entity entity)
        {
            
        }
    }
}