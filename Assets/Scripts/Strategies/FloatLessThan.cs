using System;
using HECSFramework.Core;
using Strategies;

namespace HECSFramework.Unity.Strategies
{
    [Serializable]
    public sealed class FloatLessThan : DilemmaDecision
    {
        [ExposeField]
        public float Value;
        
        [Connection(ConnectionPointType.In, "<float> Float")]
        public GenericNode<float> Float;
        public override string TitleOfNode { get; } = "FloatLessThan";

        protected override void Run(Entity entity)
        {
            if (Float.Value(entity) < Value)
            {
                Positive.Execute(entity);
                return;
            }

            Negative.Execute(entity);
        }
    }
}