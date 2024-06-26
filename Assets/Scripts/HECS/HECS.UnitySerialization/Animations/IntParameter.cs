namespace HECSFramework.Serialize
{
    public sealed partial class IntParameter : AnimatorParameter<int>
    {
        protected override void SetValueToAnimator()
        {
            base.SetValueToAnimator();
            Animator.SetInteger(ParameterAnimatorHashCode, Value);
        }
    }
}