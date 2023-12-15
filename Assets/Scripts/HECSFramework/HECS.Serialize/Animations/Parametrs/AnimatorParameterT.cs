namespace HECSFramework.Serialize
{
    public abstract partial class AnimatorParameter<T> : AnimatorParameter 
    {
        public T Value { get; protected set; }

        protected AnimatorParameter(int parameterID) : base(parameterID)
        {
        }

        public AnimatorParameter (int parameterID, T value) : base(parameterID)
        {
            Value = value;
        }

        public void Set(T value, bool force = false)
        {
            //if (!force && value.Equals(Value)) return;
            IsDirty = true;
            LocalSet(value);
            SetValueToAnimator();
        }

        protected abstract void LocalSet(T value);
        protected virtual void SetValueToAnimator() { }
    }
}