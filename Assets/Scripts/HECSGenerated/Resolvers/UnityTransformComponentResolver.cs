using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public partial struct UnityTransformComponentResolver : IResolver<UnityTransformComponent>, IResolver<UnityTransformComponentResolver,UnityTransformComponent>, IData	{		public UnityTransformComponentResolver In(ref UnityTransformComponent unitytransformcomponent)		{			return this;		}		public void Out(ref Entity entity)		{			var local = entity.GetComponent<UnityTransformComponent>();			Out(ref local);		}		public void Out(ref UnityTransformComponent unitytransformcomponent)		{		}	}}