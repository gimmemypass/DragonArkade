using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject]
    [HECSManualResolver(typeof(WaitNode))]
    public struct WaitNodeResolver : IResolver<WaitNodeResolver, WaitNode>
    {
        [Key(0)]
        public float WaitTime;
        [Key(1)]
        public float MaxWaitTime;

        public WaitNodeResolver In(ref WaitNode data)
        {
            WaitTime = data.WaitTime;
            MaxWaitTime = data.MaxWaitTime;
            return this;
        }

        public void Out(ref WaitNode data)
        {
            data.WaitTime = WaitTime;
            data.MaxWaitTime = MaxWaitTime;
        }
    }
}