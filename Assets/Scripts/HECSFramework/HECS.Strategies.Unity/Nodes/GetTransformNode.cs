using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "this node provides access to unity transform, we can convert it to Vector3 position")]
    public class GetTransformNode : GenericNode<Transform>
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional Entity")]
        public GenericNode<Entity> AdditionalEntity;

        public override string TitleOfNode { get; } = "GetTransformNode";

        [Connection(ConnectionPointType.Out, " <Transform> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
        }

        public override Transform Value(Entity entity)
        {
            var needed = this.AdditionalEntity != null ? AdditionalEntity.Value(entity) : entity;
            return needed.GetComponent<UnityTransformComponent>().Transform;
        }

        [GetConvertNodeAttribute]
        public Vector3 Position(Entity entity)
        {
            return Value(entity).position;
        }
    }
}
