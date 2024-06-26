namespace HECSFramework.Serialize
{
    public sealed partial class IntParameter : AnimatorParameter<int>
    {
        public IntParameter(int parametrName) : base(parametrName)
        {
        }

        public IntParameter(int parameterID, int value) : base(parameterID, value)
        {
        }

        protected override void LocalSet(int value)
        {
            Value = value;
        }
    }
}