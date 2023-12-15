using HECSFramework.Serialize;
using MessagePack;
using Strategies;

namespace HECSFramework.Core
{
    [MessagePackObject]
    [HECSManualResolver(typeof(UpdateStateNode))]
    public struct UpdateStateNodeResolver : IResolver<UpdateStateNodeResolver, UpdateStateNode>
    {
        public UpdateStateNodeResolver In(ref UpdateStateNode data)
        {
            return this;
        }

        public void Out(ref UpdateStateNode data)
        {
        }
    }
}