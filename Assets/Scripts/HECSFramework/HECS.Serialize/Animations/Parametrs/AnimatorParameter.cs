namespace HECSFramework.Serialize
{
    public abstract partial class AnimatorParameter
    {
        public int ParameterAnimatorHashCode { get; protected set; }    
        public bool IsDirty;

        public AnimatorParameter(int parameterID)
        {
            ParameterAnimatorHashCode = parameterID;
        }
    }
}