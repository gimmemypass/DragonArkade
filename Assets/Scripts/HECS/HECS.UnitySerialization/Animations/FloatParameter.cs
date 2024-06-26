using HECSFramework.Serialize;

namespace HECSFramework.Serialize
{
    public sealed partial class FloatParameter : AnimatorParameter<float>
    {
        protected override void SetValueToAnimator()
        {
            base.SetValueToAnimator();
            Animator.SetFloat(ParameterAnimatorHashCode, Value);
        }
    }
}