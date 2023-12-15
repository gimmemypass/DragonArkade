using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject]
    [HECSManualResolver(typeof(StartDecision))]
    public struct StartDecisionResolver : IResolver<StartDecisionResolver, StartDecision>
    {
        public StartDecisionResolver In(ref StartDecision data)
        {
            return this;
        }

        public void Out(ref StartDecision data)
        {

        }
    }
}