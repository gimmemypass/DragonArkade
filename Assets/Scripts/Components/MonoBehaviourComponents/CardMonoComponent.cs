using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class CardMonoComponent : MonoBehaviour
    {
        public Button button;
        public Image IconImage;
        [SerializeField]
        private CanvasGroup canvasGroup;

        private void OnEnable()
        {
            canvasGroup.alpha = 0;
            var canvasGroupTransform = (canvasGroup.transform as RectTransform);
            canvasGroupTransform.localPosition = new Vector2(0, -canvasGroupTransform.sizeDelta.y);
            canvasGroupTransform.DOLocalMove(Vector3.zero, 0.25f);
            canvasGroup.DOFade(1, 0.25f);
        }

        private void OnDisable()
        {

        }

        public async UniTask Close()
        {
            var canvasGroupTransform = (canvasGroup.transform as RectTransform);
            await UniTask.WhenAll(new UniTask[]
            {
                canvasGroupTransform.DOLocalMoveY(canvasGroupTransform.sizeDelta.y, 0.25f).ToUniTask(),
                canvasGroup.DOFade(0, 0.25f).ToUniTask()
            });
        }
    }
}