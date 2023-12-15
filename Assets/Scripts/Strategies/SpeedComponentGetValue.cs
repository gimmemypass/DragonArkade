using Components;
using HECSFramework.Core;

namespace Strategies
{
    public sealed class SpeedComponentGetValue : GenericNode<float>
    {
        public override string TitleOfNode { get; } = "SpeedComponentGetValue";
		
        [Connection(ConnectionPointType.Out, "<float> Out")]
        public BaseDecisionNode Out;
        public override void Execute(Entity entity)
        {
        }
        public override float Value(Entity entity)
        {
            return entity.GetComponent<SpeedComponent>().Value;
        }
    }
}