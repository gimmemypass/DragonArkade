using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public partial struct ExplodeSpheresAbilityComponentResolver : IResolver<ExplodeSpheresAbilityComponent>, IResolver<ExplodeSpheresAbilityComponentResolver,ExplodeSpheresAbilityComponent>, IData	{		public ExplodeSpheresAbilityComponentResolver In(ref ExplodeSpheresAbilityComponent explodespheresabilitycomponent)		{			return this;		}		public void Out(ref Entity entity)		{			var local = entity.GetComponent<ExplodeSpheresAbilityComponent>();			Out(ref local);		}		public void Out(ref ExplodeSpheresAbilityComponent explodespheresabilitycomponent)		{		}	}}