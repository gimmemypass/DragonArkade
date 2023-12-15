namespace HECSFramework.Serialize
{
    public sealed partial class BoolParameter : AnimatorParameter<bool>
    {
        public BoolParameter(int parametrName) : base(parametrName)
        {
        }

        public BoolParameter(int parameterID, bool value) : base(parameterID, value)
        {
        }

        protected override void LocalSet(bool value)
        {
            Value = value;
        }
    }
}