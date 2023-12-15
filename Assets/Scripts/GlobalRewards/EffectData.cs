using Components;
using HECSFramework.Core;
using UnityEngine;

namespace VFX
{
    public struct EffectData
    {
        public int VfxId;
        public Vector3 From;
        public Vector3 To;
        public Vector2 Size;
        public int ItemsCount;
        public CollectConfig VfxConfig;
        public Entity Sender;
        public int CanvasId;
    }
}