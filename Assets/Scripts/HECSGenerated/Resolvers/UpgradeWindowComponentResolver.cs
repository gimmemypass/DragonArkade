using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public partial struct UpgradeWindowComponentResolver : IResolver<UpgradeWindowComponent>, IResolver<UpgradeWindowComponentResolver,UpgradeWindowComponent>, IData	{		public UpgradeWindowComponentResolver In(ref UpgradeWindowComponent upgradewindowcomponent)		{			return this;		}		public void Out(ref Entity entity)		{			var local = entity.GetComponent<UpgradeWindowComponent>();			Out(ref local);		}		public void Out(ref UpgradeWindowComponent upgradewindowcomponent)		{		}	}}