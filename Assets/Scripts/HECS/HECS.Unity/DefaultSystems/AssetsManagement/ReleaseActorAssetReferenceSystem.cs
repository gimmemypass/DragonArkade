using System;
using System.Threading.Tasks;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "listen to disposing actor and release by assetServiceSystem and pooling system")]
    public sealed class ReleaseActorAssetReferenceSystem : BaseSystem, IReactGlobalCommand<ActorViewDisposedCommand>, IGlobalStart
    {
        private AssetsServiceSystem assetService;

        public override void InitSystem()
        {
        }

        public void GlobalStart()
        {
            assetService = EntityManager.Default.GetSingleSystem<AssetsServiceSystem>();
        }

        public void CommandGlobalReact(ActorViewDisposedCommand command)
        {
            if (assetService.TryGetContainerFast<ActorViewReference, GameObject>(command.Actor
                    .GetHECSComponent<ViewReferenceComponent>().ViewReference, out var container))
            {
                container.ReleaseInstance(command.Actor.gameObject);
                assetService.ReleaseContainer(container);               
            }
        }
    }
}