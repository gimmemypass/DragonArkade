using System;
using Components;
using HECSFramework.Serialize;
using MessagePack;

namespace Components
{
    [HECSDefaultResolver]
    public partial class ActorContainerID
    {
    }
}

namespace HECSFramework.Core
{
    [MessagePackObject, Serializable]
	public struct ActorContainerIDResolver : IResolver<ActorContainerID>, IResolver<ActorContainerIDResolver, ActorContainerID>, IData
	{
		[Key(0)]
		public string ID;

		public ActorContainerIDResolver In(ref ActorContainerID actorcontainerid)
		{
			this.ID = actorcontainerid.ID;
			return this;
		}
		public void Out(ref ActorContainerID actorcontainerid)
		{
			actorcontainerid.ID = this.ID;
		}

        public void Out(ref Entity entity)
        {
			var local = entity.GetComponent<ActorContainerID>();
			Out(ref local);
        }
    }
}