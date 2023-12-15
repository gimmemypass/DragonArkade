using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public partial struct HealthComponentResolver : IResolver<HealthComponent>, IResolver<HealthComponentResolver,HealthComponent>, IData	{		public HealthComponentResolver In(ref HealthComponent healthcomponent)		{			return this;		}		public void Out(ref Entity entity)		{			var local = entity.GetComponent<HealthComponent>();			Out(ref local);		}		public void Out(ref HealthComponent healthcomponent)		{		}	}}