using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject]
    [HECSManualResolver(typeof(CompleteFinalNode))]
    public struct CompleteFinalNodeResolver : IResolver<CompleteFinalNodeResolver, CompleteFinalNode>
    {
        public CompleteFinalNodeResolver In(ref CompleteFinalNode data)
        {
            return this;
        }

        public void Out(ref CompleteFinalNode data)
        {
        }
    }
}