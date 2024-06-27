using System;
using System.Threading.Tasks;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Cysharp.Threading.Tasks;
using Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "drop item system")]
    public sealed class DropItemSystem : BaseSystem, IReactCommand<TriggerEnterCommand>
    {
        [Required] public DropItemComponent DropItemComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(TriggerEnterCommand command)
        {
            if (command.Collider.TryGetActorFromCollision(out var actor) && actor != null &&
                actor.Entity.ContainsMask<CharacterItemsComponent>())
            {
                SpawnItem(actor).Forget();
            } 
        }

        private async UniTask SpawnItem(Actor actor)
        {
            var item = await DropItemComponent.ItemContainer.GetActor();
            item.Init();
            actor.Command(new AddItemToCharacterCommand() { Item = item.Entity });
            Owner.HecsDestroy();
        }
    }
}