using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public partial struct ActionsWithPredicateHolderComponentResolver : IResolver<ActionsWithPredicateHolderComponent>, IResolver<ActionsWithPredicateHolderComponentResolver,ActionsWithPredicateHolderComponent>, IData	{		public ActionsWithPredicateHolderComponentResolver In(ref ActionsWithPredicateHolderComponent actionswithpredicateholdercomponent)		{			return this;		}		public void Out(ref Entity entity)		{			var local = entity.GetComponent<ActionsWithPredicateHolderComponent>();			Out(ref local);		}		public void Out(ref ActionsWithPredicateHolderComponent actionswithpredicateholdercomponent)		{		}	}}