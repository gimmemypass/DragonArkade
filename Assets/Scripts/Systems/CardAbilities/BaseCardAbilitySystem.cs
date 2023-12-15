using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;

namespace Systems
{
    public abstract class BaseCardAbilitySystem : BaseAbilitySystem
    {
        public override void InitSystem()
        {
        }

        public override async void Execute(Entity owner = null, Entity target = null, bool enable = true)
        {
            var price = Owner.GetComponent<EnergyComponent>().Value;
            var energyComponent = EntityManager.Default.GetSingleComponent<MainCharacterTagComponent>().Owner.GetComponent<EnergyComponent>();
            energyComponent.ChangeValue(-price);
            
            await ExecuteCard(owner, target, enable);
            
            EntityManager.Default.Command(new CardAbilityHandledCommand());
        }

        public abstract UniTask ExecuteCard(Entity owner = null, Entity target = null, bool enable = true);
    }
}