using System;
using System.Collections.Generic;
using AssetsManagement.Containers;
using HECSFramework.Core;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Helpers, "Global system to managing assets")]
    public sealed class AssetsServiceSystem : BaseSystem
    {
        private const int MAX_RETRY_DELAY = 60;

        private readonly Dictionary<string, UniTask> assetsLoadsMap = new();
        private readonly Dictionary<string, object> assetsContainersCache = new();
        private readonly Dictionary<string, int> containersRefsCount = new();

        private int exceptionsCount;

        public override void InitSystem()
        {
        }

        public async UniTask<AssetRefContainer<TRef, TObject>> GetContainer<TRef, TObject>(TRef reference)
            where TRef : AssetReference
            where TObject : Object
        {
            if (assetsLoadsMap.TryGetValue(reference.AssetGUID, out var value))
            {
                await value;
            }
            else if (!assetsContainersCache.ContainsKey(reference.AssetGUID))
            {
                await PreloadContainer<TRef, TObject>(reference);
            }

            containersRefsCount[reference.AssetGUID]++;
            return assetsContainersCache[reference.AssetGUID] as AssetRefContainer<TRef, TObject>;
        }

        public bool TryGetContainerFast<TRef, TObject>(TRef reference, out AssetRefContainer<TRef, TObject> container)
            where TRef : AssetReference
            where TObject : Object
        {
            if (assetsContainersCache.TryGetValue(reference.AssetGUID, out var obj))
            {
                container = obj as AssetRefContainer<TRef, TObject>;
                return true;
            }

            container = default;
            return false;
        }

        public void ReleaseContainer<TRef, TObject>(AssetRefContainer<TRef, TObject> refContainer)
            where TRef : AssetReference
            where TObject : Object
        {
            TRef reference = refContainer.Reference;
            if (!assetsContainersCache.ContainsKey(reference.AssetGUID))
            {
                Debug.LogError($"Cannot find container with provided ref {reference}");
                return;
            }

            containersRefsCount[reference.AssetGUID]--;

            int assetsInstancesRefsCount = refContainer.RefsCount;
            int assetContainerRefsCount = containersRefsCount[reference.AssetGUID];

            if (assetsInstancesRefsCount == 0 && assetContainerRefsCount == 0)
            {
                containersRefsCount.Remove(reference.AssetGUID);
                assetsContainersCache.Remove(reference.AssetGUID);
                reference.ReleaseAsset();
            }
        }

        private async UniTask PreloadContainer<TRef, TObject>(
            TRef reference,
            UniTaskCompletionSource loadingTCS = null
        )
            where TRef : AssetReference
            where TObject : Object
        {
            if (loadingTCS == null)
            {
                loadingTCS = new UniTaskCompletionSource();
                assetsLoadsMap[reference.AssetGUID] = loadingTCS.Task.Preserve();
            }

            try
            {
                if(!reference.IsDone || !reference.IsValid())
                    await reference.LoadAssetAsync<TObject>().ToUniTask();

                exceptionsCount = 0;
                AssetRefContainer<TRef, TObject> refContainer = new AssetRefContainer<TRef, TObject>(reference);
                assetsContainersCache[reference.AssetGUID] = refContainer;
                containersRefsCount[reference.AssetGUID] = 0;

                assetsLoadsMap.Remove(reference.AssetGUID);
                loadingTCS.TrySetResult();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                exceptionsCount++;
                int delayTime = Mathf.Clamp((int)Mathf.Pow(2, exceptionsCount), 0, MAX_RETRY_DELAY) * 1000;
                await UniTask.Delay(delayTime);
                await PreloadContainer<TRef, TObject>(reference, loadingTCS);
            }
        }
    }
}