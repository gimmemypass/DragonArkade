using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Helpers.UI
{
    public class FillBar
    {
        private readonly Image barImage;
        
        private Vector2 fillBarSize;
        private Tween tween;

        public FillBar(Image barImage)
        {
            this.barImage = barImage;
        }

        public void Init()
        {
            fillBarSize = barImage.rectTransform.sizeDelta;
        }
        
        public void SetBarSize(float normalizedProgress, float duration = .35f, float delay = 0f)
        {
            var sizeX = MathExtensions.GetClampedValueFromZeroToOne(normalizedProgress,
                0, 1,
                0, fillBarSize.x);
            var newSize = new Vector2(sizeX, fillBarSize.y);
            tween?.Kill();
            tween = barImage.rectTransform.DOSizeDelta(newSize, duration).SetDelay(delay);
            tween.Play();
        }

        public void CleanUp()
        {
            tween?.Kill();
        }
    }
}