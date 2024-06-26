using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "we provide vector3 from here")]
    public class Vector3Node : GenericNode<Vector3>
    {
        public override string TitleOfNode { get; } = "Vector3Node";

        [ExposeField]
        public Vector3 Vector3;

        [Connection(ConnectionPointType.Out, " <Vector3> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public override Vector3 Value(Entity entity)
        {
            return Vector3;
        }
    }
}