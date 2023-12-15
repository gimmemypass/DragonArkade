using HECSFramework.Serialize;

namespace HECSFramework.Serialize
{
    public sealed partial class FloatParameter : AnimatorParameter<float>
    {
        public FloatParameter(int parametrName) : base(parametrName)
        {
        }

        public FloatParameter(int parameterID, float value) : base(parameterID, value)
        {
        }

        protected override void LocalSet(float value)
        {
            Value = value;
        }
    }
}
