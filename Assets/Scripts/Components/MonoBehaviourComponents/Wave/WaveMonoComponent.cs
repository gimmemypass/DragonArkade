using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class WaveMonoComponent : MonoBehaviour
    {
        private IWaveElement[] waveElements;

        private void Awake()
        {
            waveElements = transform.GetComponentsInChildren<IWaveElement>();
        }

        public async UniTask Prepare()
        {
            await UniTask.WhenAll(waveElements.Select(a => a.Prepare()));
        }
        public async UniTask Finish()
        {
            await UniTask.WhenAll(waveElements.Select(a => a.Finish()));
        }
    }

    public interface IWaveElement
    {
        public UniTask Prepare();
        public UniTask Finish();
    }
}