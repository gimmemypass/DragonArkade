using Components;
using HECSFramework.Core;

public class CooldownPredicate : IPredicate
{
    public bool IsReady(Entity target, Entity owner = null)
    {
        var component = target.GetComponent<CooldownComponent>();
        return component.Value <= 0;
    }
}