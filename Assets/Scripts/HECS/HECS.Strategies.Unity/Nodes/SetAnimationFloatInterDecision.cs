using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, "SetAnimationFloatInterDecision")]
    public class SetAnimationFloatInterDecision : InterDecision
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional (Animator state owner)")]
        public GenericNode<Entity> AnimatorOwner;

        [Connection(ConnectionPointType.In, "<float> Value")]
        public GenericNode<float> FloatValue;

        [AnimParameterDropDown]
        public int AnimParameter;

        [ExposeField]
        public bool Force;

        public override string TitleOfNode { get; } = "Set Animation Float";

        protected override void Run(Entity entity)
        {
            var animOwner = AnimatorOwner != null ? AnimatorOwner.Value(entity) : entity;

            animOwner.GetComponent<AnimatorStateComponent>().State.SetFloat(AnimParameter, FloatValue.Value(entity), Force);
            Next.Execute(entity);
        }
    }
}
