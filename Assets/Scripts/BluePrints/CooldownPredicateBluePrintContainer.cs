using Components;
using HECSFramework.Unity;
using UnityEngine;

namespace BluePrints
{
    [CreateAssetMenu(menuName = "BluePrints/Predicates/CooldownPredicateBluePrintContainer", fileName = "CooldownPredicateBluePrintContainer", order = 0)]
    public class CooldownPredicateBluePrintContainer : PredicateBluePrintContainer<CooldownPredicate>
    {
    }
}