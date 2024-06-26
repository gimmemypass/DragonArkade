using Commands;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, Doc.UniversalNodes, "we need this node when we want ")]
    public sealed class ExecuteAbilityOnEntity : InterDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Ability Owner")]
        public GenericNode<Entity> EntityWithAbility;

        [Connection(ConnectionPointType.In, "<Entity> Target")]
        public GenericNode<Entity> Target;

        [AbilityIDDropDown]
        public int AbilityIndex;

        [ExposeField]
        public bool Enable;

        [ExposeField]
        public bool IgnorePredicates;

        public override string TitleOfNode { get; } = "ExecuteAbilityOnEntity";

        protected override void Run(Entity entity)
        {
            EntityWithAbility.Value(entity).Command(new ExecuteAbilityByIDCommand
            {
                AbilityIndex = this.AbilityIndex,
                Enable = Enable,
                IgnorePredicates = IgnorePredicates,
                Owner = EntityWithAbility.Value(entity),
                Target = Target.Value(entity),
            });

            Next.Execute(entity);
        }
    }
}