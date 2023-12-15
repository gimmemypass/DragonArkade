using System;
using HECSFramework.Core;
using Strategies;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

namespace HECSFramework.Unity.Strategies
{
    [Serializable]
    public sealed class CheckLuck : DilemmaDecision
    {
        [ExposeField]
        public float Chance; 
        public override string TitleOfNode { get; } = "CheckLuck";

        protected override void Run(Entity entity)
        {
            if (Random.value < Chance)
            {
                Positive.Execute(entity);
                return;
            }

            Negative.Execute(entity);
        }
    }
}