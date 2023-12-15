using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using VFX;
using Random = Unity.Mathematics.Random;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Visual, Doc.FX, "Control logic of collect items like fx to ui with big amount of particles")]
    public sealed class MultipleCollectItemsSystem : BaseSystem, IReactGlobalCommand<MultipleItemsFxCommand>
    {
        [Single] public VFXCreationSystem VFXCreationSystem;

        public override void InitSystem()
        {
        }

        public void CommandGlobalReact(MultipleItemsFxCommand command)
        {
            ProcessMultipleMovementItemsForUI(command.EffectData, command.OnSingleItemComplete,
                command.OnAllItemsComplete).Forget();
        }

        private async UniTask ProcessMultipleMovementItemsForUI(EffectData effectData, Action onSingleItemComplete,
            Action onAllItemsComplete)
        {
            effectData.Sender?.AddComponent<VisualInActionTagComponent>();
            EffectPlayerForCollectToUI[] effects = await GetEffectsForCollectionFromPool(effectData);
            List<UniTask> tasks = Enumerable.Select(effects, item => PlayEffect(item, onSingleItemComplete)).ToList();
            await UniTask.WhenAll(tasks);
            effectData.Sender?.RemoveComponent<VisualInActionTagComponent>();
            onAllItemsComplete?.Invoke();
        }

        private async UniTask PlayEffect(EffectPlayerForCollectToUI item, Action callback)
        {
            await item.Play();
            item.Release();
            callback?.Invoke();
        }

        private async UniTask<EffectPlayerForCollectToUI[]> GetEffectsForCollectionFromPool(EffectData effectData)
        {
            List<UniTask<EffectPlayerForCollectToUI>> tasks = new();
            for (int i = 0; i < effectData.ItemsCount; i++)
            {
                tasks.Add(VFXCreationSystem.GetEffectForCollectUI(effectData));
            }

            return await UniTask.WhenAll(tasks);
        }
    }
}