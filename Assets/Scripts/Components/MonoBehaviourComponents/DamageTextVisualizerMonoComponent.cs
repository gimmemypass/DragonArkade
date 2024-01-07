using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class DamageTextVisualizerMonoComponent : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private Tween visualizeAnimation;
        private void Start()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            var animations = GetComponentInChildren<DOTweenAnimation>();
            var tweens = animations.GetTweens();
            var sequence = DOTween.Sequence();
            foreach (var tween in tweens)
            {
                sequence.Join(tween);
            }
            sequence.Rewind();
            visualizeAnimation = sequence;
            text.alpha = 0;
        }

        public async UniTask Visualize(float damageAmount)
        {
            text.alpha = 1;
            text.text = $"-{damageAmount.ToString()}";
            await visualizeAnimation.Play().ToUniTask();
        } 
    }
}