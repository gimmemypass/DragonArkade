using System;
using Components;
using Components.MonoBehaviourComponents;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class DropPartsHealthVisualSystem : BaseSystem, IUpdatable, IHaveActor
    {
        [Required] public HealthComponent HealthComponent;
        public Actor Actor { get; set; }
        private DropPartsHealthVisualMonoComponent monoComponent;
        private int activatedParts;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
            activatedParts = monoComponent.Parts.Count;
        }

        public void UpdateLocal()
        {
            var count = monoComponent.Parts.Count;
            var delta = HealthComponent.CalculatedMaxValue / count;
            var visualCount = 0;
            for (int i = 0; i < count + 1; i++)
            {
                if (HealthComponent.Value > delta * i) continue;
                visualCount = i;
                break;
            }

            if (activatedParts > visualCount)
            {
                for (int i = count - 1; i >= visualCount - 1; i--)
                {
                    var part = monoComponent.Parts[i];
                    if(part.TryGetComponent(out MeshRenderer meshRenderer))
                    {
                        meshRenderer.enabled = false;
                    }
                    if(part.TryGetComponent(out Collider collider))
                    {
                        collider.enabled = false;
                    }
                    foreach (var col in part.GetComponentsInChildren<Collider>())
                    {
                        col.enabled = false;
                    }
                    foreach (var mr in part.GetComponentsInChildren<MeshRenderer>())
                    {
                        mr.enabled = false;
                    }
                    part.gameObject.GetComponentInChildren<ParticleSystem>()?.Play();
                }

                activatedParts = visualCount;
            }
        }
    }
}