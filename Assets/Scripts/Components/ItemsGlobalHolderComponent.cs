using System;
using System.Linq;
using HECSFramework.Unity;
using HECSFramework.Core;
using HECSFramework.Unity.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ItemsGlobalHolderComponent : BaseComponent, IWorldSingleComponent
    {
        [SerializeField]
        private ActorContainer[] containers;

        [SerializeField]
        private ActorContainer[] dropContainers;

        public ActorContainer GetByContainerId(int id)
        {
            foreach (var container in containers)
            {
                if (container.ContainerIndex == id)
                    return container;
            }
            throw new Exception();
        }

        public ActorContainer GetDropByContainerId(int id)
        {
            foreach (var container in dropContainers)
            {
                if (container.ContainerIndex == id)
                    return container;
            }
            throw new Exception();           
        }

        public ActorContainer GetRandomDrop() => dropContainers.GetRandomElement();
        public ActorContainer GetRandomItem() => containers.GetRandomElement();
    }
}