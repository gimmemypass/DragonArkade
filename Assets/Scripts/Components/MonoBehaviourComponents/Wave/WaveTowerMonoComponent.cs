using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class WaveTowerMonoComponent : MonoBehaviour, IWaveElement
    {
        private float OFFSET = 10f;

        public async UniTask Prepare()
        {
            transform.position += transform.right * OFFSET;
            await transform.DOMove(transform.position - transform.right * OFFSET, 0.5f).SetEase(Ease.OutQuad).ToUniTask();
        }

        public async UniTask Finish()
        {
            await transform.DOMove(transform.position + transform.right * OFFSET, 0.5f).SetEase(Ease.OutQuad).ToUniTask();
        }
    }
}