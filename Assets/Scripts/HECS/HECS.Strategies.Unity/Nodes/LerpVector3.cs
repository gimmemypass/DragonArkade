using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "LerpVector3")]
    public class LerpVector3 : GenericNode<Vector3>
    {
        [Connection(ConnectionPointType.In, " <Vector3> From")]
        public GenericNode<Vector3> From;
        
        [Connection(ConnectionPointType.In, " <Vector3> To")]
        public GenericNode<Vector3> To;

        [Connection(ConnectionPointType.In, "<float> Progress")]
        public GenericNode<float> Progress;

        public override string TitleOfNode { get; } = "Lerp Vector3";

        [Connection(ConnectionPointType.Out, " <Vector3> Out")]
        public BaseDecisionNode Out;

        public override void Execute(Entity entity)
        {
        }

        public override Vector3 Value(Entity entity)
        {
            return Vector3.Lerp(From.Value(entity), To.Value(entity), Progress.Value(entity));
        }
    }
}
