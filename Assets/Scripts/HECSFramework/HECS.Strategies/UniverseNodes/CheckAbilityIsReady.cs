using System.Runtime.CompilerServices;
using Components;
using HECSFramework.Core;
using Strategies;

[Documentation(Doc.Strategy, Doc.HECS, "here we check predicates of needed ability, we try to take it from generic node, if entity not provided, we try to take ability from main strategy entity")]
public sealed class CheckAbilityIsReady : DilemmaDecision
{
    [Connection(ConnectionPointType.In, "<Entity> Target")]
    public GenericNode<Entity> Target;

    [Connection(ConnectionPointType.In, "<Entity> Ability Owner")]
    public GenericNode<Entity> AbilityOwner;

    public override string TitleOfNode { get; } = "Check Ability Is Ready";

    [AbilityIDDropDown]
    public int AbilityIndex;

    protected override void Run(Entity entity)
    {
        var abilityOwner = AbilityOwner?.Value(entity);

        if (abilityOwner != null)
        {
            if (!Ready(abilityOwner, entity))
            {
                Negative.Execute(entity);
                return;
            }
        }
        else
        {
            if (!Ready(entity, entity))
            {
                Negative.Execute(entity);
                return;
            }
        }

        Positive.Execute(entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool Ready(Entity abilityOwner, Entity logicEntity)
    {
        if (abilityOwner.TryGetComponent(out AbilitiesHolderComponent abilitiesHolderComponent))
        {
            if (abilitiesHolderComponent.IndexToAbility.TryGetValue(AbilityIndex, out var ability))
            {
                if (ability.TryGetComponent(out AbilityPredicateComponent predicatesComponent))
                {
                    if (Target != null)
                    {
                        if (!predicatesComponent.TargetPredicates.IsReady(Target.Value(logicEntity), ability))
                        {
                            return false;
                        }
                    }

                    if (!predicatesComponent.AbilityPredicates.IsReady(ability))
                    {
                        return false;
                    }

                    if (!predicatesComponent.AbilityOwnerPredicates.IsReady(abilityOwner, Target?.Value(logicEntity)))
                    {
                        return false;
                    }
                }
            }
        }
        
        return true;
    }
}