using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.HECS, "this node return vector direction")]
    public sealed class DirectionNode : GenericNode<Vector3>
    {
        [Connection(ConnectionPointType.In, " <Vector3> To")]
        public GenericNode<Vector3> To;

        [Connection(ConnectionPointType.In, " <Vector3> From")]
        public GenericNode<Vector3> From;

        public override string TitleOfNode { get; } = "DirectionNode";

        [Connection(ConnectionPointType.Out, " <Vector3> Out")]
        public BaseDecisionNode Out;

        [ExposeField]
        public bool Normilized = false;


        public override void Execute(Entity entity)
        {
        }

        public override Vector3 Value(Entity entity)
        {
            if (Normilized)
                return (To.Value(entity) - From.Value(entity)).normalized;
            else
                return To.Value(entity) - From.Value(entity);
        }
    }
}
