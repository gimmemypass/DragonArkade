using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.UniversalNodes, Doc.Strategy, "this node return is entity alive or not")]
    public class IsEntityAlive : DilemmaDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> optional entity")]
        public GenericNode<Entity> OptionalEntity;

        public override string TitleOfNode { get; } = "IsEntityAlive";

        protected override void Run(Entity entity)
        {
            if (OptionalEntity != null) 
            {
                if (OptionalEntity.Value(entity).IsAlive())
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

            if (entity.IsAlive())
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