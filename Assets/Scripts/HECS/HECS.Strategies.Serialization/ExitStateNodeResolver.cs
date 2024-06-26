using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject]
    [HECSManualResolver(typeof(ExitStateNode))]
    public struct ExitStateNodeResolver : IResolver<ExitStateNodeResolver, ExitStateNode>
    {
        public ExitStateNodeResolver In(ref ExitStateNode data)
        {
            return this;
        }

        public void Out(ref ExitStateNode data)
        {

        }
    }
}