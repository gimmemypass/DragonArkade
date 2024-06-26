using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.HECS, Doc.UniversalNodes, "this node return position of Unity transform node")]
    public sealed class GetPositionUnityNode : GenericNode<Vector3>
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional Entity")]
        public GenericNode<Entity> AdditionalEntity;

        public override string TitleOfNode { get; } = "GetPositionUnityNode";

        [Connection(ConnectionPointType.Out, "<Vector3> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public override Vector3 Value(Entity entity)
        {
            if (AdditionalEntity != null)
                return AdditionalEntity.Value(entity).GetComponent<UnityTransformComponent>().Transform.position;
            else
                return entity.GetComponent<UnityTransformComponent>().Transform.position;
        }
    }
}
