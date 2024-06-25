using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, Doc.Arena, "Control arena battle indicator")]
    public sealed class ArenaBattleIndicatorSystem : BaseSystem, IUpdatable, IGlobalStart, IHaveActor
    {
        private const float INDICATOR_OFFSET = 6f;
        private const float INDICATOR_SCREEN_OFFSET = 0.1f;
        private const float INDICATOR_SPEED = 30;
        public Actor Actor { get; set; }

        private IndicatorMonoComponent indicatorMonoComponent;
        private Camera camera;
        private Entity mainCharacter;

        public override void InitSystem()
        {
            Actor.TryGetComponent(out indicatorMonoComponent);
        }

        public void GlobalStart()
        {
            camera = EntityManager.Default.GetSingleComponent<MainCameraComponent>().Camera;
        }

        public void UpdateLocal()
        {
            var target = MainCharacter?.GetComponent<TargetEntityComponent>().Target;
            if (target is not { IsAlive: true })
            {
                indicatorMonoComponent.Indicator.gameObject.SetActive(false);
                return;
            }
            var targetPos = target.GetComponent<UnityTransformComponent>().Transform.position + Vector3.up * INDICATOR_OFFSET;
            var screenPos = camera.WorldToScreenPoint(targetPos);
            // var isInvisible = screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 ||
                            // screenPos.y > Screen.height;
            screenPos.x = Math.Clamp(screenPos.x, Screen.width * INDICATOR_SCREEN_OFFSET, Screen.width*(1 - INDICATOR_SCREEN_OFFSET));
            screenPos.y = Math.Clamp(screenPos.y, Screen.height * INDICATOR_SCREEN_OFFSET, Screen.height * (1 - INDICATOR_SCREEN_OFFSET));
            indicatorMonoComponent.Indicator.position = Vector3.Lerp(indicatorMonoComponent.Indicator.position,
                screenPos, INDICATOR_SPEED*Time.deltaTime);
            var dir = screenPos - new Vector3(Screen.width / 2f, Screen.height / 2f);
            var rot = Vector2.SignedAngle(Vector2.up, dir);
            indicatorMonoComponent.Indicator.rotation = Quaternion.Euler(0,0,rot);
            if (!indicatorMonoComponent.Indicator.gameObject.activeSelf)
            {
                indicatorMonoComponent.Indicator.position = screenPos;
                indicatorMonoComponent.Indicator.gameObject.SetActive(true);
            }
        }

        private Entity MainCharacter
        {
            get
            {
                if (mainCharacter is { IsAlive: true }) return mainCharacter;
                
                mainCharacter = EntityManager.Default.TryGetSingleComponent(
                    out MainCharacterTagComponent mainCharacterTagComponent) ? mainCharacterTagComponent.Owner : null;
                return mainCharacter;
            }
        }
    }
}