using System;
using System.Threading;
using System.Threading.Tasks;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "default attack ability system")]
    public sealed class DragonDefaultAttackAbilitySystem : BaseAbilitySystem
    {
        [Required] public CooldownComponent CooldownComponent;
        private ItemsGlobalHolderComponent itemsGlobalHolderComponent;

        public override void InitSystem()
        {
            itemsGlobalHolderComponent = EntityManager.Default.GetSingleComponent<ItemsGlobalHolderComponent>();
        }

        public override void Execute(Entity owner = null, Entity target = null, bool enable = true)
        {
            ExecuteAsync(owner).Forget();
        }

        private async UniTask ExecuteAsync(Entity owner)
        {
            HECSDebug.Log("Attack");
            if (Owner.ContainsMask<VisualInActionTagComponent>())
                throw new Exception("Ability already in action, add predicate to avoid execution");
            Owner.AddComponent<VisualInActionTagComponent>();
            var position = owner.GetComponent<CharacterItemsSourceMonoComponentProvider>().Get.transform.position;
            var actor = await SpawnAttackItem(owner, position);
            await actor.transform.DOScale(Vector3.one, 0.25f);

            actor.transform.SetParent(null, true);
            actor.Command(new TryApplyItemCommand());
            CooldownComponent.SetValue(CooldownComponent.CalculatedMaxValue);
            Owner.RemoveComponent<VisualInActionTagComponent>();
        }

        private async UniTask<Actor> SpawnAttackItem(Entity owner, Vector3 position)
        {
            var item = itemsGlobalHolderComponent.GetByContainerId(EntityContainersMap.FireballContainer);
            var actor = await item.GetActor(position: position, transform: owner.GetComponent<UnityTransformComponent>().Transform);
            actor.transform.localScale = Vector3.zero;
            actor.Init();
            actor.GetOrAddHECSComponent<BelongingComponent>().Entity = owner;
            return actor;
        }
    }
}