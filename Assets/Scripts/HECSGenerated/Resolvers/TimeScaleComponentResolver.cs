using Components;using System;using MessagePack;using HECSFramework.Serialize;using Commands;namespace HECSFramework.Core{	[MessagePackObject, Serializable]	public partial struct TimeScaleComponentResolver : IResolver<TimeScaleComponent>, IResolver<TimeScaleComponentResolver,TimeScaleComponent>, IData	{		public TimeScaleComponentResolver In(ref TimeScaleComponent timescalecomponent)		{			return this;		}		public void Out(ref Entity entity)		{			var local = entity.GetComponent<TimeScaleComponent>();			Out(ref local);		}		public void Out(ref TimeScaleComponent timescalecomponent)		{		}	}}