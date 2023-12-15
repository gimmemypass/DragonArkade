using System;
using Components;
using HECSFramework.Core;
using Strategies;
using UnityEngine;

namespace BluePrints.Strategies
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class MoveToScreenViewNode : InterDecision
    {
        [ExposeField]
        public float OffsetInPercent; //0 - Screen.width / 2f, 0.5 - Screen.width / 2f * 0.5
        [Connection(ConnectionPointType.In, "Speed")]
        public GenericNode<float> Speed;
        public override string TitleOfNode { get; } = "RB Move To Screen Space Node";

        protected override void Run(Entity entity)
        {
            var position = entity.GetComponent<UnityTransformComponent>().Transform.position;
            var camera = EntityManager.Default.GetSingleComponent<MainCameraComponent>().Camera;
            var screenPos = camera.WorldToScreenPoint(position);
            var isVisible = screenPos.x <= Screen.width * (1 - OffsetInPercent / 2f) &&
                            screenPos.x > Screen.width / 2f * OffsetInPercent &&
                            screenPos.y > Screen.height / 2f * OffsetInPercent &&
                            screenPos.y <= Screen.height * (1 - OffsetInPercent / 2f);
            if (isVisible)
                return;
            
            var centerPos = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f));
            var speed = Speed.Value(entity);
            var rb = entity.GetComponent<RigidbodyProviderComponent>().Get;
            var dir = centerPos - position;
            dir.y = 0;
            rb.MovePosition(rb.position + dir.normalized * (speed * 2 * Time.deltaTime));

            Next.Execute(entity);
        }
    }
}