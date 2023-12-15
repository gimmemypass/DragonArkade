using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject]
    [HECSManualResolver(typeof(DebugDecision))]
    public struct DebugDecisionResolver : IResolver<DebugDecisionResolver, DebugDecision>
    {
        [Key(0)]
        public string DebugMessage;

        [Key(1)]
        public bool PrintEntityGUID;
        public DebugDecisionResolver In(ref DebugDecision data)
        {
            DebugMessage = data.DebugMessage;
            PrintEntityGUID = data.PrintEntityGUID;
            return this;
        }

        public void Out(ref DebugDecision data)
        {
            data.DebugMessage = DebugMessage;
            data.PrintEntityGUID = PrintEntityGUID;
        }
    }
}