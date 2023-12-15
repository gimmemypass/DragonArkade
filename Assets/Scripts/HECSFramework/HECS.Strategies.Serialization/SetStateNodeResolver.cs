using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject()]
    [HECSManualResolver(typeof(SetStateNode))]
    public struct SetStateNodeResolver : IResolver<SetStateNodeResolver, SetStateNode>
    {
        [Key(0)] public byte[] State;

        public SetStateNodeResolver In(ref SetStateNode data)
        {
            State = EntityManager.ResolversMap.SerializeCustom(data.State);
            return this;
        }

        public void Out(ref SetStateNode data)
        {
            data.State = EntityManager.ResolversMap.Deserialize<State>(State);
        }
    }
}