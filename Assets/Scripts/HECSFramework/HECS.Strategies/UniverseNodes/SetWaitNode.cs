using Components;
using HECSFramework.Core;

namespace Strategies
{
    public sealed class SetWaitNode : SetNode
    {
        public override string TitleOfNode { get; } = "SetWaitNode by float";

        [Connection(ConnectionPointType.In, "<float> In")]
        public GenericNode<float> Time;

        protected override void Run(Entity entity)
        {
            entity.GetOrAddComponent<WaitStateComponent>().CurrentWaitTimer = Time.Value(entity); ;
        }
    }
}
