using Components;
using HECSFramework.Core;


namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, "this node checkRemain distance and make decision we reach navmesh destination or not")]
    public class NavmeshReachDistance : DilemmaDecision
    {
        public override string TitleOfNode { get; } = "NavmeshReachDistance";

        [ExposeField]
        public float RemainingDistance = 1;

        protected override void Run(Entity entity)
        {
            var navmeshAgent  = entity.GetComponent<NavMeshAgentComponent>().NavMeshAgent;

            if (navmeshAgent.remainingDistance < RemainingDistance)
            {
                Positive.Execute(entity);
                return;
            }
            else
            {
                Negative.Execute(entity);
                return;
            }
        }
    }
}
