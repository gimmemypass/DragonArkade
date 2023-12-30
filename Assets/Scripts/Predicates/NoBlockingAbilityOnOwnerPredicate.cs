using Components;
using HECSFramework.Core;

public class NoBlockingAbilityOnOwnerPredicate : IPredicate
{
    public bool IsReady(Entity target, Entity owner = null)
    {
        return !target.ContainsMask<BlockingAbilityInActionComponent>();
    }
}