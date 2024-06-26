using System.Collections.Generic;
using UnityEngine;

namespace HECSFramework.Serialize
{
    public partial class AnimatorState
    {
        public void SetAnimator(Animator animator)
        {
            SetAnimatorToAnimParameter(boolParameters, animator);
            SetAnimatorToAnimParameter(floatParameters, animator);
            SetAnimatorToAnimParameter(intParameters, animator);
        }

        private void SetAnimatorToAnimParameter<T>(Dictionary<int, T> dictionary, Animator animator) where T: AnimatorParameter
        {
            foreach (var anim in dictionary)
            {
                anim.Value.Animator = animator;
            }
        }

        public void ApplyParametersToAnimator()
        {
            foreach (var parameter in boolParameters.Values)
            {
                parameter.Animator.SetBool(parameter.ParameterAnimatorHashCode, parameter.Value);
            }
            foreach (var parameter in floatParameters.Values)
            {
                parameter.Animator.SetFloat(parameter.ParameterAnimatorHashCode, parameter.Value);
            }
            foreach (var parameter in intParameters.Values)
            {
                parameter.Animator.SetInteger(parameter.ParameterAnimatorHashCode, parameter.Value);
            }
        }
    }
}
