using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject()]
    [HECSManualResolver(typeof(SetDefaultStrategyNode))]
    public struct SetDefaultStrategyNodeResolver : IResolver<SetDefaultStrategyNodeResolver, SetDefaultStrategyNode>
    {
        public SetDefaultStrategyNodeResolver In(ref SetDefaultStrategyNode data)
        {
            return this;
        }

        public void Out(ref SetDefaultStrategyNode data)
        {
        }
    }
}