using Components;
using HECSFramework.Core;

public class VisualNotInActionPredicate : IPredicate
{
    public bool IsReady(Entity target, Entity owner = null)
    {
        return !target.ContainsMask<VisualInActionTagComponent>();
    }
}