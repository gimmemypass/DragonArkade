using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.UI, "soft value counter")]
    public sealed class SoftValueUISystem : BaseSystem, IHaveActor, IUpdatable
    {
        public Actor Actor { get; set; }
        private SoftValueCounterComponent softValueComponent;
        
        private SoftValueUIMonoComponent monoComponent;
        private int prevValue;

        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent, true);
            softValueComponent = EntityManager.Default.GetSingleComponent<PlayerTagComponent>().Owner
                .GetComponent<SoftValueCounterComponent>();
            UpdateVisual();
        }

        public void UpdateLocal()
        {
            if (prevValue == softValueComponent.Value)
                return;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            prevValue = softValueComponent.Value;
            monoComponent.SetCounterValue(softValueComponent.Value.ToString());
        }
    }
}