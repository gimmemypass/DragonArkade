using MessagePack;
using System;
using System.Numerics;

namespace HECSFramework.Core
{
    [MessagePackObject, Serializable]
    public partial struct Vector3Serialize : IEquatable<Vector3Serialize>
    {
        [Key(0)]
        public float X;
        [Key(1)]
        public float Y;
        [Key(2)]
        public float Z;

        public Vector3Serialize(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3Serialize(Vector3 vector3)
        {
            X = vector3.X;
            Y = vector3.Y;
            Z = vector3.Z;
        }

        public Vector3Serialize(Vector2Serialize vector2)
        {
            X = vector2.X;
            Y = 0;
            Z = vector2.Y;
        }

        public Vector3 AsNumericsVector() 
            => new Vector3(X, Y, Z);

        public Vector2 AsNumericsVector2() 
            => new Vector2(X, Z);

        [IgnoreMember]
        public static Vector3Serialize Zero => new Vector3Serialize(0, 0, 0);
        
        [IgnoreMember]
        public static Vector3Serialize One => new Vector3Serialize(0, 0, 0);

        public override string ToString()
            => $"({X}, {Y}, {Z})";

        public bool Equals(Vector3Serialize other)
        {
            return Math.Abs(X - other.X) < 0.0001 && Math.Abs(Y - other.Y) < 0.0001 && Math.Abs(Z - other.Z) < 0.0001;
        }

        public override int GetHashCode()
        {
            int hashCode = -307843816;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }
        [IgnoreMember]
        public float sqrMagnitude => X * X + Y * Y + Z * Z;
        public static Vector3Serialize operator -(Vector3Serialize left, Vector3Serialize right)
        {
            return new Vector3Serialize(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }
        public static Vector3Serialize operator +(Vector3Serialize left, Vector3Serialize right)
        {
            return new Vector3Serialize(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }
        public static Vector3Serialize operator *(Vector3Serialize source, float value)
        {
            return new Vector3Serialize(source.X * value, source.Y * value, source.Z * value);
        }
        public static Vector3Serialize operator /(Vector3Serialize source, float value)
        {
            return new Vector3Serialize(source.X / value, source.Y / value, source.Z / value);
        }
    }
}