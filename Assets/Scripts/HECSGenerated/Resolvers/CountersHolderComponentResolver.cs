using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public partial struct CountersHolderComponentResolver : IResolver<CountersHolderComponent>, IResolver<CountersHolderComponentResolver,CountersHolderComponent>, IData	{		public CountersHolderComponentResolver In(ref CountersHolderComponent countersholdercomponent)		{			countersholdercomponent.BeforeSync();			return this;		}		public void Out(ref Entity entity)		{			var local = entity.GetComponent<CountersHolderComponent>();			Out(ref local);		}		public void Out(ref CountersHolderComponent countersholdercomponent)		{			countersholdercomponent.AfterSync();		}	}}