using System;
using System.Linq;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using DG.Tweening;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class IndicateDamageSystem : BaseSystem, IHaveActor, IReactCommand<DamageForVisualFXCommand>
    {
        private MeshRenderer[] meshRenderers;
        private SkinnedMeshRenderer[] skinnedMeshRenderers;
        
        private MaterialPropertyBlock materialProperty;
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private Tween tween;
        public Actor Actor { get; set; }
        public override void InitSystem()
        {
            meshRenderers = Actor.GetComponentsInChildren<MeshRenderer>().Where(a => a.gameObject.activeSelf).ToArray();
            skinnedMeshRenderers = Actor.GetComponentsInChildren<SkinnedMeshRenderer>()
                .Where(a => a.gameObject.activeSelf).ToArray();
            materialProperty = new MaterialPropertyBlock();
        }

        public void CommandReact(DamageForVisualFXCommand command)
        {
            tween?.Kill();
            var seq = DOTween.Sequence();
            seq
                .Append(DOVirtual.Color(materialProperty.GetColor(BaseColor), Color.red, 0.5f, SetColor))
                .Append(DOVirtual.Color(Color.red, Color.white, 0.5f, SetColor));
            tween = seq;
        }

        private void SetColor(Color value)
        {
            materialProperty.SetColor(BaseColor, value);
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.SetPropertyBlock(materialProperty, 0); 
            }

            foreach (var m in skinnedMeshRenderers)
            {
                m.SetPropertyBlock(materialProperty, 0);
            }
        }
    }
}