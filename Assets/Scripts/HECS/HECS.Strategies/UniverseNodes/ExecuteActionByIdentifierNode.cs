using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, "this node execute identifiers by action holder and action identifier")]
    public class ExecuteActionByIdentifierNode : InterDecision
    {
        public override string TitleOfNode { get; } = "Execute action by identifier";

        [DropDownIdentifier("ActionIdentifier")]
        public int ActionIdentifier;

        [Connection(ConnectionPointType.In, "<Entity> In")]
        public GenericNode<Entity> GetEntityNode;

        protected override void Run(Entity entity)
        {
            if (GetEntityNode != null)
            {
               var needed =  GetEntityNode.Value(entity);

                if (needed.TryGetComponent(out ActionsHolderComponent actionsHolderComponent))
                    actionsHolderComponent.ExecuteAction(ActionIdentifier);
            }
            else
            {
                if (entity.TryGetComponent(out ActionsHolderComponent actionsHolderComponent))
                    actionsHolderComponent.ExecuteAction(ActionIdentifier);
            }

            Next.Execute(entity);
        }
    }
}