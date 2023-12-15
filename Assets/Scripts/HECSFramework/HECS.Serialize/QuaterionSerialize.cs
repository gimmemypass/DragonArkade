using MessagePack;
using System;
using System.Numerics;

namespace HECSFramework.Core
{
    [MessagePackObject, Serializable]
    public struct QuaternionSerialize
    {
        [Key(0)]
        public float W;
        [Key(1)]
        public float X;
        [Key(2)]
        public float Y;
        [Key(3)]
        public float Z;

        public QuaternionSerialize(float w, float x, float y, float z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        public Quaternion AsQuaternion() => new Quaternion(X, Y, Z, W);
    }
}
