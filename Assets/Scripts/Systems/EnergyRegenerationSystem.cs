using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class EnergyRegenerationSystem : BaseSystem, ICustomUpdatable
    {
        [Required] public EnergyRegenerationComponent EnergyRegenerationComponent;
        [Required] public EnergyComponent EnergyComponent;
        public YieldInstruction Interval { get; } = new WaitForSeconds(1);

        public override void InitSystem()
        {
        }

        public void UpdateCustom()
        {
            EnergyComponent.ChangeValue(EnergyRegenerationComponent.Value);
        }

    }
}