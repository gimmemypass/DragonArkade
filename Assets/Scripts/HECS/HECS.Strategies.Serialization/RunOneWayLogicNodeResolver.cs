using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject]
    [HECSManualResolver(typeof(RunOneWayLogicNode))]
    public struct RunOneWayLogicNodeResolver : IResolver<RunOneWayLogicNodeResolver, RunOneWayLogicNode>
    {
        [Key(0)] public byte[] OneWayLogicData;
        public RunOneWayLogicNodeResolver In(ref RunOneWayLogicNode data)
        {
            OneWayLogicData = EntityManager.ResolversMap.SerializeCustom(data.Logic);
            return this;
        }

        public void Out(ref RunOneWayLogicNode data)
        {
            data.Logic = EntityManager.ResolversMap.Deserialize<OneWayLogic>(OneWayLogicData);
        }
    }
}