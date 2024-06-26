using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Animation, Doc.Strategy, "here we set animation trigger parameter")]
    public class SetAnimationTriggerParametr : InterDecision
    {
        [AnimParameterDropDown]
        public int TriggerIdentifier;
    

        public override string TitleOfNode { get; } = "SetAnimationTriggerParametr";

        protected override void Run(Entity entity)
        {
            entity.GetComponent<AnimatorStateComponent>().SetTrigger(TriggerIdentifier);
            Next.Execute(entity);
        }
    }
}