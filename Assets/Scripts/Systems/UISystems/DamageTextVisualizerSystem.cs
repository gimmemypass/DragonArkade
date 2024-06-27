using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;
using Cysharp.Threading.Tasks;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.UI, "system for visualize damage text")]
    public sealed class DamageTextVisualizerSystem : BaseSystem, IHaveActor, IReactCommand<DamageForVisualFXCommand>, IUpdatable
    {
        public Actor Actor { get; set; }
        [Required] public DamageTextVisualizerComponent DamageTextVisualizerComponent;

        private DamageTextVisualizerMonoComponent monoComponent;
        private Camera camera;
        private Vector3 worldPos;

        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
            camera = EntityManager.Default.GetSingleComponent<MainCameraComponent>().Camera;
        }

        public void CommandReact(DamageForVisualFXCommand command)
        {
            worldPos = command.Victim.GetComponent<HealthBarPlaceMonoComponentProvider>().Get.transform.position;
            Visualize(command).Forget();
        }

        private async UniTask Visualize(DamageForVisualFXCommand command)
        {
            await UniTask.DelayFrame(1);
            await monoComponent.Visualize(command.DamageAfterResist);
            Owner.HecsDestroy();
        }

        public void UpdateLocal()
        {
            UpdatePos();
        }

        private void UpdatePos()
        {
            var screenPos = camera.WorldToScreenPoint(worldPos);
            screenPos.z = 0;
            monoComponent.transform.position = screenPos;
        }
    }
}