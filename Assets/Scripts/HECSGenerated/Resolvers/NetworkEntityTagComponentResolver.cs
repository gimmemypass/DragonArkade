using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public partial struct NetworkEntityTagComponentResolver : IResolver<NetworkEntityTagComponent>, IResolver<NetworkEntityTagComponentResolver,NetworkEntityTagComponent>, IData	{		public NetworkEntityTagComponentResolver In(ref NetworkEntityTagComponent networkentitytagcomponent)		{			return this;		}		public void Out(ref Entity entity)		{			var local = entity.GetComponent<NetworkEntityTagComponent>();			Out(ref local);		}		public void Out(ref NetworkEntityTagComponent networkentitytagcomponent)		{		}	}}