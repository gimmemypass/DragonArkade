using HECSFramework.Core;

namespace Strategies
{
    public class UpdateStateNode : LogNode
    {
        public override string TitleOfNode { get; } = "Update";
        
        [Connection(ConnectionPointType.Out, "Update every tick")]
        public BaseDecisionNode Update;

        protected override void Run(Entity entity)
        {
            Update?.Execute(entity);
        }
    }
}