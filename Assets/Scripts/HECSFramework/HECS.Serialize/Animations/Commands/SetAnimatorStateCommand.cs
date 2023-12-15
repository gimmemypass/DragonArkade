using System;
using HECSFramework.Serialize;

namespace Commands
{
    public struct SetAnimatorStateCommand<T> where T : struct, ISetAnimatorState
    {
        public T SetState;
    }

    public interface ISetAnimatorState
    {
        void SetState(AnimatorState animatorState);
    }

    [Serializable]
    public struct BoolId
    {
        public bool Value;
        public int Id;
    }

    [Serializable]
    public struct IntId
    {
        public int Value;
        public int Id;
    }

    [Serializable]
    public struct FloatId
    {
        public float Value;
        public int Id;
    }
}