using System.Threading.Tasks;
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

        public override void Execute(Entity owner = null, Entity target = null, bool enable = true)
        {
            ExecuteAsync(owner, target, enable).Forget();
        }

        private async UniTask ExecuteAsync(Entity owner, Entity target, bool enable)
        {
            var price = Owner.GetComponent<EnergyComponent>().Value;
            var energyComponent = EntityManager.Default.GetSingleComponent<MainCharacterTagComponent>().Owner
                .GetComponent<EnergyComponent>();
            energyComponent.ChangeValue(-price);

            await ExecuteCard(owner, target, enable);

            EntityManager.Default.Command(new CardAbilityHandledCommand());
        }

        public abstract UniTask ExecuteCard(Entity owner = null, Entity target = null, bool enable = true);
    }
}