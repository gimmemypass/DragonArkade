using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject]
    [HECSManualResolver(typeof(OneWayLogicExitNode))]
    public struct OneWayLogicExitNodeResolver : IResolver<OneWayLogicExitNodeResolver, OneWayLogicExitNode>
    {
        public OneWayLogicExitNodeResolver In(ref OneWayLogicExitNode data)
        {
            return this;
        }

        public void Out(ref OneWayLogicExitNode data)
        {
        }
    }
}