using System;
using Commands;
using Components;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class MainCharLevelingSystem : BaseSystem, IGlobalStart, IReactGlobalCommand<TransitionGameStateCommand>
    {
        [Required] public HealthComponent HealthComponent;
        [Required] public EnergyComponent EnergyComponent;
        private LevelModifierHolderComponent levelModifierHolderComponent;

        public override void InitSystem()
        {
            
        }

        public void GlobalStart()
        {
            AsSingle(ref levelModifierHolderComponent);
            HealthComponent.AddModifier(Owner.GUID,
                levelModifierHolderComponent.MainCharHealth.GetModifierWithCast<IModifier<float>>());
            EnergyComponent.AddModifier(Owner.GUID,
                levelModifierHolderComponent.MainCharEnergy.GetModifierWithCast<IModifier<float>>());
        }

        public void CommandGlobalReact(TransitionGameStateCommand command)
        {
            if (command.To == GameStateIdentifierMap.PrepareBattle)
            {
                HealthComponent.Recalc();
                EnergyComponent.Recalc();
            }  
        }
    }
}