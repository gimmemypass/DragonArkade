using System;
using System.Collections.Generic;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class CardsGlobalHolderComponent : BaseComponent, IWorldSingleComponent
    {
        [SerializeField]
        private ActorContainer[] cardContainers;

        public bool TryFromContainerId(int id, out ActorContainer container)
        {
            foreach (var cardContainer in cardContainers)
            {
                if (cardContainer.ContainerIndex == id)
                {
                    container = cardContainer;
                    return true;
                } 
            }

            container = default;
            return false;
        }

        public IEnumerable<ActorContainer> Containers => cardContainers;
    }
}