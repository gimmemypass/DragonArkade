using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public partial struct LevelModifierHolderComponentResolver : IResolver<LevelModifierHolderComponent>, IResolver<LevelModifierHolderComponentResolver,LevelModifierHolderComponent>, IData	{		public LevelModifierHolderComponentResolver In(ref LevelModifierHolderComponent levelmodifierholdercomponent)		{			return this;		}		public void Out(ref Entity entity)		{			var local = entity.GetComponent<LevelModifierHolderComponent>();			Out(ref local);		}		public void Out(ref LevelModifierHolderComponent levelmodifierholdercomponent)		{		}	}}