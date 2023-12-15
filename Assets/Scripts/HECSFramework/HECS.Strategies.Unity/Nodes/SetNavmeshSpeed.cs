using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, "SetNavmeshAgentDistance")]
    public class SetNavmeshSpeed : InterDecision
    {
        [Connection(ConnectionPointType.In, "<float> In")]
        public GenericNode<float> GetSpeed;

        public override string TitleOfNode { get; } = "SetNavmeshSpeed";

        protected override void Run(Entity entity)
        {
            if (entity.TryGetComponent(out NavMeshAgentComponent navMeshAgentComponent))
            {
                navMeshAgentComponent.NavMeshAgent.speed = GetSpeed.Value(entity);
            }
            else
                HECSDebug.LogError("we dont have navmesh agent on entity " + entity.ContainerID);

            Next.Execute(entity);
        }
    }
}
