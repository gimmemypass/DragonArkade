using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VFX;
using Random = UnityEngine.Random;

namespace Components
{
    public class CollectItem : MonoBehaviour
    {
        public bool IsPlaying { get; set; }

        [SerializeField] private CollectConfig _defaultConfig;

        private RectTransform RectTransform => (RectTransform)transform;

        private Camera mainCamera;
        private CollectTypeFly currentTypeFly;
        private Vector3 from;
        private Vector3 to;
        private Vector3 size;

        private float randomPerpPos;
        private float randomPerpLenght;

        private CollectConfig collectConfig;
        private float duration;
        private float delayStartTime;

        private readonly List<Tweener> tweens = new List<Tweener>();

        private Sequence sequence;

        private void Awake()
        {
            InitDefault();
        }

        public void Construct(EffectData data)
        {
            collectConfig = data.VfxConfig;
            from = data.From;
            to = data.To;
            size = data.Size;
            
            duration = data.VfxConfig.DelayTime.GetRandomBetween();
            delayStartTime = data.VfxConfig.DelayStartTime.GetRandomBetween();
            randomPerpPos = data.VfxConfig.PosPerp.GetRandomBetween();
            randomPerpLenght = data.VfxConfig.LenghtPerp.GetRandomBetween();
        }

        public void InitDefault()
        {
            randomPerpLenght = _defaultConfig.LenghtPerp.GetRandomBetween();
            randomPerpPos = _defaultConfig.PosPerp.GetRandomBetween();
            duration = _defaultConfig.DelayTime.GetRandomBetween();
            delayStartTime = _defaultConfig.DelayStartTime.GetRandomBetween();

            collectConfig = _defaultConfig;
        }

        public void Init()
        {
            transform.position = from;
            RectTransform.sizeDelta = size;
            transform.localScale = Vector3.one * collectConfig.ScaleProgress.Evaluate(0);
        }

        public async UniTask MoveToWithCalculateCubicBezier(Vector3 from, Vector3 to, TweenCallback finishCallback = null,
            CancellationToken cancellationToken = default)
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            
            Vector3[] cubicBezierPath = GetPath(from, to);

            var moveTween = transform.DOPath(cubicBezierPath, duration, PathType.CubicBezier)
                .SetDelay(delayStartTime).SetEase(collectConfig.ProgressCurve).OnComplete(finishCallback);

            Vector3 startScale = collectConfig.RelativeOneScale ? Vector3.one : RectTransform.localScale;
            
            var scaleTween = DOVirtual.Float(collectConfig.ProgressCurve.keys[0].value,
                collectConfig.ProgressCurve.keys.Last().value, duration, val => UpdateScale(val, startScale));

            sequence.Append(moveTween);
            sequence.Join(scaleTween);

            cancellationToken.ThrowIfCancellationRequested();

            await sequence.AsyncWaitForCompletion();
            
            cancellationToken.ThrowIfCancellationRequested();
        }

        private Vector3[] GetPath(Vector3 from, Vector3 to)
        {
            Vector3[] targetPoints = { to };
            Vector3[] cubicBezierPath = CalculateCubicBezierPath(from, to, targetPoints);
            return cubicBezierPath;
        }

        private void OnDestroy() =>
            Cleanup();

        private void UpdateScale(float value, Vector3 startScale)
        {
            var scale = collectConfig.ScaleProgress.Evaluate(value) * startScale;
            transform.localScale = scale;
        }

        private Vector3 Perpendicular(Vector3 from, Vector3 to)
        {
            Vector3 delta = to - from;
            Vector3 randomDotOnVector = from + delta * randomPerpPos;

            var dir = Random.Range(0, 1) == 0 ? 1 : -1;
            Vector3 perpendicular =
                new Vector3(Vector2.Perpendicular(delta).x * dir, Vector2.Perpendicular(delta).y, 0);
            return randomDotOnVector + perpendicular * randomPerpLenght;
        }

        private Vector3[] CalculateCubicBezierPath(Vector3 from, Vector3 to, Vector3[] path)
        {
            var result = new Vector3[path.Length * 3];

            for (int i = 0; i < path.Length; i++)
            {
                int j = i * 3;
                var targetPoint = path[i];

                var a = Perpendicular(from, targetPoint);
                var b = Perpendicular(targetPoint, to);

                var controlPoints = new[] { a, b };
                from = targetPoint;
                result[j] = targetPoint;
                result[j + 1] = controlPoints[0];
                result[j + 2] = controlPoints[1];
            }

            return result;
        }

        public void Cleanup()
        {
            foreach (var tween in tweens)
            {
                tween.Kill();
            }
            sequence?.Kill();
            tweens.Clear();
        }
    }
}