using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject]
    [HECSManualResolver(typeof(ChainEnd))]
    public struct ChainEndNodeResolver : IResolver<ChainEndNodeResolver, ChainEnd>
    {
        public ChainEndNodeResolver In(ref ChainEnd data)
        {
            return this;
        }

        public void Out(ref ChainEnd data)
        {
        }
    }
}