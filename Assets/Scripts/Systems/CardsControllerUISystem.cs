using System;
using System.Linq;
using System.Threading.Tasks;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using Components;
using Components.MonoBehaviourComponents;
using Cysharp.Threading.Tasks;
using HECSFramework.Unity.Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class CardsControllerUISystem : BaseSystem, IHaveActor, IReactGlobalCommand<CardActivatedCommand>
    {
        private const int CardsCount = 3;
        private CardsGlobalHolderComponent cardsHolder;

        private CardsControllerMonoComponent monoComponent;
        private EntitiesFilter cards;
        public Actor Actor { get; set; }
        public override void InitSystem()
        {
            AsSingle(ref cardsHolder);
            Actor.TryGetComponent(out monoComponent, true);
            cards = EntityManager.Default.GetFilter<CardTagComponent>();
            AddDeck();
        }

        private async UniTask<Actor> AddCard(int pushRightCardContainer)
        {
            cardsHolder.TryFromContainerId(pushRightCardContainer, out var container);
            return await AddCard(container);
        }

        private async UniTask<Actor> AddCard(ActorContainer container)
        {
            var card = await container.GetActor(transform: monoComponent.Parent);
            card.TryGetComponent(out CardMonoComponent cardMonoComponent);
            cardMonoComponent.IconImage.sprite = card.GetHECSComponent<IconComponent>().Icon;
            card.Init();
            return card;
        }

        public async void CommandGlobalReact(CardActivatedCommand command)
        {
            var transform = command.Entity.GetComponent<UnityTransformComponent>().Transform;
            var siblingIndex = transform.GetSiblingIndex();
            CloseCard(command).Forget();
            foreach (var cardContainer in cardsHolder.Containers.OrderBy(a => Guid.NewGuid()))
            {
                var alreadyInHands = false;
                foreach (var cardEnt in cards)
                {
                    if (cardEnt.GetComponent<ActorContainerID>().ContainerIndex !=
                        cardContainer.ContainerIndex) continue;
                    alreadyInHands = true;
                    break;
                }
                if(alreadyInHands)
                    continue;
                var card = await AddCard(cardContainer);
                card.transform.SetSiblingIndex(siblingIndex);
                break;
            }

        }

        private void AddDeck()
        {
            var cardContainers = cardsHolder.Containers.OrderBy(a => Guid.NewGuid()).ToArray();
            for (int i = 0; i < CardsCount; i++)
            {
                AddCard(cardContainers[i]).Forget();
            }
        }

        private static async UniTaskVoid CloseCard(CardActivatedCommand command)
        {
            command.Entity.AsActor().TryGetComponent(out CardMonoComponent monoComponent);
            monoComponent.transform.SetParent(monoComponent.transform.parent.parent);
            await monoComponent.Close();
            command.Entity.HecsDestroy();
        }
    }
}