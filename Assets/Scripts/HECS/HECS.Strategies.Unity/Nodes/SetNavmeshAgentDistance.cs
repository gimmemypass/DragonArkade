using Components;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, "SetNavmeshAgentDistance")]
    public class SetNavmeshAgentDistance : InterDecision
    {
        [Connection(ConnectionPointType.In, "<Vector3> In")]
        public GenericNode<Vector3> GetDistance;
        
        public override string TitleOfNode { get; } = "SetNavmeshAgentDistance";

        protected override void Run(Entity entity)
        {
            if (entity.TryGetComponent(out NavMeshAgentComponent navMeshAgentComponent))
            {
                navMeshAgentComponent.SetDestination(GetDistance.Value(entity));
            }
            else
                HECSDebug.LogError("we dont have navmesh agent on entity " + entity.ContainerID);

           Next.Execute(entity);
        }
    }
}
