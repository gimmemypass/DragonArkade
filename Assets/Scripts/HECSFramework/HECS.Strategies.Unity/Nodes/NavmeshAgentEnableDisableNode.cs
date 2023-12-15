using Components;
using HECSFramework.Core;


namespace Strategies
{
    [Documentation(Doc.Strategy, "NavmeshAgentEnableDisableNode")]
    public sealed class NavmeshAgentEnableDisableNode : InterDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional Entity")]
        public GenericNode<Entity> AdditionalEntity;

        public override string TitleOfNode { get; } = "NavmeshAgentEnableDisableNode";

        [ExposeField]
        public bool Enable = false;

        protected override void Run(Entity entity)
        {
            var needed = AdditionalEntity != null ? AdditionalEntity.Value(entity) : entity;

            needed.GetComponent<NavMeshAgentComponent>().NavMeshAgent.enabled = Enable;
            Next.Execute(entity);
        }
    }
}
