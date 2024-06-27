using System;
using System.Threading.Tasks;
using Commands;
using Components;
using HECSFramework.Core;
using Cysharp.Threading.Tasks;
using HECSFramework.Unity;
using UnityEngine;
using VFX;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Visual, Doc.FX, "System that creates ready to use effect")]
    public sealed class VFXCreationSystem : BaseSystem, IReactGlobalCommand<SpawnFXCommand>
    {
        private PoolFxGlobalSystem fxPool;

        public override void InitSystem()
        {
            fxPool = Owner.GetSystem<PoolFxGlobalSystem>();
        }

        public void CommandGlobalReact(SpawnFXCommand command)
        {
            SpawnFXAsync(command).Forget();
        }

        private async UniTask SpawnFXAsync(SpawnFXCommand command)
        {
            GameObject effect = await fxPool.GetEffectById(command.VfxId);

            effect.transform.position = command.Position;
            if (command.Parent != null)
            {
                effect.transform.SetParent(command.Parent);
                effect.transform.localPosition = Vector3.zero;
            }

            effect.transform.localRotation = command.LocalRotation;
            effect.transform.localScale = command.Scale;

            command.OnSpawnedCallback?.Invoke(effect);
        }

        public async UniTask<EffectPlayerForCollectToUI> GetEffectForCollectUI(EffectData data)
        {
            GameObject effect = await fxPool.GetEffectById(data.VfxId);

            Entity canvasEntity = GetCanvas(data.CanvasId);
            Transform canvasRoot = canvasEntity.AsActor().transform;
            
            effect.transform.SetParent(canvasRoot);
            
            PoolingSystem poolSystem = Owner.World.GetSingleSystem<PoolingSystem>();
            EffectPlayerForCollectToUI effectPlayerForCollectToUI =
                new EffectPlayerForCollectToUI(data, effect, poolSystem);
            return effectPlayerForCollectToUI;
        }

        public async UniTask<GameObject> GetEffect(int vfxId) =>
            await fxPool.GetEffectById(vfxId);

        private Entity GetCanvas(int canvasId)
        {
            return Owner.World.GetFilter<AdditionalCanvasTagComponent>()
                .FirstOrDefault(x => x.GetComponent<AdditionalCanvasTagComponent>()
                    .AdditionalCanvasIdentifier.Id == canvasId);
        }
    }
}