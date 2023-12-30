using System;
using System.Linq;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

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
            await UniTask.Delay(200);
            for (int i = 0; i < 6; i++)
            {
                var fireballActor = await SpawnAttackItem(abilityOwner);
                Owner.Command(new AddItemToCharacterCommand()
                {
                    Item = fireballActor.Entity
                }); 
            }
            
            foreach (var item in CharacterItemsComponent.Items.Values)
            {
                item.AsActor().transform.DOScale(Vector3.one, 0.5f);
            }
            
            Owner.AddComponent<VisualInActionTagComponent>();
            await new FireballRingJob(){CharacterItemsComponent = CharacterItemsComponent}.RunJob();
            await UniTask.Delay(1000);
            
            Owner.RemoveComponent<VisualInActionTagComponent>();
            abilityOwner.RemoveComponent<BlockingAbilityInActionComponent>();


        }
        
        private async UniTask<Actor> SpawnAttackItem(Entity owner)
        {
            var item = itemsGlobalHolderComponent.GetByContainerId(EntityContainersMap.FireballContainer);
            var actor = await item.GetActor();
            actor.transform.localScale = Vector3.zero;
            actor.Init();
            actor.GetOrAddHECSComponent<BelongingComponent>().Entity = owner;
            return actor;
        }

        private struct FireballRingJob : IHecsJob
        {
            public CharacterItemsComponent CharacterItemsComponent;
            public void Run()
            {
            }

            public bool IsComplete()
            {
                return CharacterItemsComponent.Items.Count == 0;
            }
        }
    }
    
}