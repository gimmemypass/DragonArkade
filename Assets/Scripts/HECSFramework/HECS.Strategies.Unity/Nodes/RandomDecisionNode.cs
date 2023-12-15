using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.HECS, "RandomDecisionNode")]
    public class RandomDecisionNode : DilemmaDecision
    {
        public override string TitleOfNode { get; } = "RandomDecisionNode";

        protected override void Run(Entity entity)
        {
            var random = Random.Range(0, 100);

            if (random > 50)
            {
                Positive.Execute(entity);
                return;
            }
            else
            {
                Negative.Execute(entity);
                return;
            }
        }
    }
}
