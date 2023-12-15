﻿using System.Collections.Generic;using HECSFramework.Core;using UnityEngine;namespace HECSFramework.Unity{	public class GameController : BaseGameController	{		[SerializeField] private ActorContainer[] additionalGlobalEntities = default;		private List<Entity> additionalEntities = new List<Entity>(8);		public override void BaseAwake()		{			foreach (var a in additionalGlobalEntities)			{				var additionlEntity = Entity.Get(a.name);				a.Init(additionlEntity);				additionalEntities.Add(additionlEntity);			}		}		public override void BaseStart()		{				foreach (var a in additionalEntities)					a.Init();		}	}}