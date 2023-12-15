using System;
using Components;
using HECSFramework.Core;
using Strategies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BluePrints.Strategies
{
    [Serializable]
    public sealed class RandomDirectionNode : InterDecision
    {
        [ExposeField] public bool X;
        [ExposeField] public bool Y;
        [ExposeField] public bool Z;
        public override string TitleOfNode { get; } = "Random Direction Node";

        protected override void Run(Entity entity)
        {
            entity.GetOrAddComponent<DirectionComponent>().Direction = new Vector3(
                X ? 2*Random.value - 1 : 0,
                Y ? 2*Random.value - 1 : 0,
                Z ? 2*Random.value - 1 : 0
            ).normalized;
            Next.Execute(entity);
        }
    }
}