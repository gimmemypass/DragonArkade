using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject()]
    [HECSManualResolver(typeof(ChangeStrategyDecision))]
    public struct ChangeStrategyDecisionNodeResolver : IResolver<ChangeStrategyDecisionNodeResolver, ChangeStrategyDecision>
    {
        [Key(0)] public byte[] Strategy;
        public ChangeStrategyDecisionNodeResolver In(ref ChangeStrategyDecision data)
        {
            Strategy = EntityManager.ResolversMap.SerializeCustom(data.Strategy);
            return this;
        }

        public void Out(ref ChangeStrategyDecision data)
        {
            data.Strategy = EntityManager.ResolversMap.Deserialize<Strategy>(Strategy);
        }
    }
}