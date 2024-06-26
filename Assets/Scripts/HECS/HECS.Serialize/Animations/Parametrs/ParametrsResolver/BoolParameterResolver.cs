using MessagePack;

namespace HECSFramework.Serialize
{
    [MessagePackObject]
    public struct BoolParameterResolver
    {
        [Key(0)]
        public bool Value;
    }

    [MessagePackObject]
    public struct IntParameterResolver
    {
        [Key(0)]
        public int Value;
    }

    [MessagePackObject]
    public struct FloatParameterResolver
    {
        [Key(0)]
        public float Value;
    }
}