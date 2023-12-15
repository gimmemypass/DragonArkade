using System;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, Doc.Visual, Doc.FX, Doc.Poolable, "This system pool fx to needed coord")]
    public sealed class PoolFxGlobalSystem : BaseSystem
    {
        [Required] public VisualFXHolderComponent fXHolderComponent;
        
        private PoolingSystem poolSystem;

        public override void InitSystem()
        {
            poolSystem = Owner.World.GetSingleSystem<PoolingSystem>();
        }

        public async UniTask<GameObject> GetEffectById(int vfxId)
        {
            if (fXHolderComponent.TryGetReferenceToFX(vfxId, out AssetReference assetReference))
            {
                var effectFromPool = await poolSystem.GetViewFromPool(assetReference);
                return effectFromPool;
            }

            HECSDebug.LogError("dont have fx poolable for id " + vfxId);
            return null;
        }
    }
}