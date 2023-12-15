using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{

    [CreateAssetMenu(menuName = "Strategies/Dilemma Strategy")]
    [Documentation(Doc.GameLogic, Doc.HECS, Doc.Strategy, "This variant of strategy have two decisions on exit, not restart of strategy, u should use it how part complex decisions, not a main strategy as well")]
    public sealed class DilemmaStrategy : Strategy
    {
        public BaseDecisionNode Positive;
        public BaseDecisionNode Negative;

        public override void Init()
        {
            if (isInited)
                return;

            base.Init();

            foreach (var n in nodes)
            {
                if (n is IPositiveDecision positiveDecision)
                {
                    positiveDecision.Positive = Positive;
                }

                if (n is INegativeDecision negativeDecision)
                {
                    negativeDecision.Negative = Negative;
                }
            }

            isInited = true;
        }
    }

    public interface IPositiveDecision
    {
        public BaseDecisionNode Positive { get; set; }
    }

    public interface INegativeDecision
    {
        public BaseDecisionNode Negative { get; set; }
    }
}