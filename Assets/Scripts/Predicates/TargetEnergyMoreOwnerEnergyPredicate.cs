using Components;
using HECSFramework.Core;

public class TargetEnergyMoreOwnerEnergyPredicate : IPredicate
{
    public bool IsReady(Entity target, Entity owner = null)
    {
        return target.GetComponent<EnergyComponent>().Value >= owner.GetComponent<EnergyComponent>().Value;
    }
}