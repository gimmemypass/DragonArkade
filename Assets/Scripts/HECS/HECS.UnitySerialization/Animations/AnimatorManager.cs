
using System.Collections.Generic;
using HECSFramework.Serialize;

namespace HECSFramework.Unity
{
    public static partial class AnimatorManager
    {
        private static Dictionary<string, AnimatorStateResolver> animStateProviders = new Dictionary<string, AnimatorStateResolver>(8);

        public static AnimatorState GetAnimatorState(string animatorName)
        {
            if (animStateProviders.TryGetValue(animatorName, out var helper))
            {
                var newState = new AnimatorState();
                newState.Load(ref helper);
                return newState;
            }
            else
                throw new System.Exception($"Doesn't have needed provider for Animator {animatorName}, probably u should run codogen");
        }
    }
}