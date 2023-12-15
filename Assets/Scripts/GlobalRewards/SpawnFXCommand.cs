using System;
using HECSFramework.Core;
using UnityEngine;

namespace Commands
{
    [Documentation(Doc.FX, Doc.Poolable, "Spawn poolable fx to coord")]
	public struct SpawnFXCommand : IGlobalCommand
	{
		public int VfxId;
		public Vector3 Position;
		public Vector3 Scale;
		public Quaternion LocalRotation;
		public Transform Parent;
		public Action<GameObject> OnSpawnedCallback;
	}
}