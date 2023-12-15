using System;
using Components;
using HECSFramework.Core;
using Strategies;
using UnityEngine;

namespace HECSFramework.Unity.Strategies
{
    [Serializable]
    public sealed class DecrementStateTimerNode : InterDecision
    {
        public override string TitleOfNode { get; } = "DecrementStateTimerNode";

        protected override void Run(Entity entity)
        {
            entity.GetComponent<StateTimerComponent>().RemainingTime -= Time.deltaTime;
            Next.Execute(entity);
        }
    }
}