using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    public sealed class RandomFloatNode : GenericNode<float>
    {
        public override string TitleOfNode { get; } = "RandomFloatNode";

        
        [Connection(ConnectionPointType.Out, "<float> Out")]
        public BaseDecisionNode Out;

        [ExposeField]
        public float MinValue = 1;
        [ExposeField]
        public float MaxValue = 1;

        public override void Execute(Entity entity)
        {
        }

        public override float Value(Entity entity)
        {
            return Random.Range(MinValue, MaxValue);
        }
    }
}