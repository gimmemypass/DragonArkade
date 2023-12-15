using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "We use this node for overlap physics node, when they provide hecs list of entities and we should take closest entity")]
    public sealed class GetClosestEntityFromHecsList : GenericNode<Entity>
    {
        public override string TitleOfNode { get; } = "GetClosestEntityFromHecsList";

        [Connection(ConnectionPointType.In, " <HECSList<Entity>> In")]
        public GenericNode<HECSList<Entity>> List;

        [Connection(ConnectionPointType.In, " <Vector3> Position")]
        public GenericNode<Vector3> Position;

        [Connection(ConnectionPointType.Out, " <Entity> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
        }

        public override Entity Value(Entity entity)
        {
            Entity closest = null;
            float distance = float.MaxValue;

            var list = List.Value(entity);
            var pos = Position.Value(entity);

            foreach (var e in list)
            {
                var dir = e.GetComponent<UnityTransformComponent>().Transform.position - pos;

                if (dir.sqrMagnitude < distance)
                {
                    distance = dir.sqrMagnitude;
                    closest = e;
                }
            }

            return closest;
        }
    }
}
