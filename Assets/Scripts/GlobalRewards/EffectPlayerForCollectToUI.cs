using Components;
using Cysharp.Threading.Tasks;
using Systems;
using UnityEngine;

namespace VFX
{
    public class EffectPlayerForCollectToUI
    {
        private readonly EffectData data;
        private readonly CollectItem collectItem;
        private readonly PoolingSystem poolingSystem;

        public EffectPlayerForCollectToUI(EffectData data, GameObject effect, PoolingSystem poolingSystem)
        {
            this.poolingSystem = poolingSystem;
            this.data = data;
            collectItem = effect.GetComponent<CollectItem>();
            collectItem.Construct(data);
        }

        public async UniTask Play()
        {
            collectItem.gameObject.SetActive(true);
            collectItem.Init();
            //todo это шляпа, тут запускаются сотни твинов, надо переделать эти эффекты на более быстрые решения
            await collectItem.MoveToWithCalculateCubicBezier(data.From, data.To);
        }

        public void Release()
        {
            if (collectItem == null)
                return;
            collectItem.Cleanup();
            poolingSystem.ReleaseView(collectItem.gameObject);
        }
    }
}