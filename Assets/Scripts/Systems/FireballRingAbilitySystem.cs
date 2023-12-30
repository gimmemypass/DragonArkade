using System;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Unity;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class FireballRingAbilitySystem : BaseAbilitySystem
    {
        [Required] public CharacterItemsComponent CharacterItemsComponent;
        private ItemsGlobalHolderComponent itemsGlobalHolderComponent;

        public override void InitSystem()
        {
            itemsGlobalHolderComponent = EntityManager.Default.GetSingleComponent<ItemsGlobalHolderComponent>();
        }

        public override async void Execute(Entity abilityOwner = null, Entity target = null, bool enable = true)
        {
            abilityOwner.AddComponent<BlockingAbilityInActionComponent>();
            for (int i = 0; i < 6; i++)
            {
                var fireballActor = await SpawnAttackItem(abilityOwner);
                Owner.Command(new AddItemToCharacterCommand()
                {
                    Item = fireballActor.Entity
                }); 
            }
            
            Owner.AddComponent<VisualInActionTagComponent>();
            // foreach (var item in CharacterItemsComponent.Items.Values)
            // {
            //     item.AsActor().transform.DOScale(Vector3.one, 0.25f);
            // }
            
        }
        
        private async UniTask<Actor> SpawnAttackItem(Entity owner)
        {
            var item = itemsGlobalHolderComponent.GetByContainerId(EntityContainersMap.FireballContainer);
            var actor = await item.GetActor();
            // actor.transform.localScale = Vector3.zero;
            actor.Init();
            actor.GetOrAddHECSComponent<BelongingComponent>().Entity = owner;
            return actor;
        }
    }
}