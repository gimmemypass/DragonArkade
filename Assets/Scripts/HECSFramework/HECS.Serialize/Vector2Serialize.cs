using System;
using System.Numerics;
using MessagePack;

namespace HECSFramework.Core
{
    [MessagePackObject, Serializable]
    public partial struct Vector2Serialize : IEquatable<Vector2Serialize>
    {
        [Key(0)]
        public float X;
        [Key(1)]
        public float Y;

        [MessagePack.SerializationConstructor]
        public Vector2Serialize(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2Serialize(Vector2 vector2)
        {
            X = vector2.X;
            Y = vector2.Y;
        }
        
        public Vector2 AsNumericsVector() 
            => new Vector2(X, Y);
        
        public Vector3 AsNumericsVector3() 
            => new Vector3(X, 0, Y);
        
        public override string ToString()
            => $"({X}, {Y})";

        [IgnoreMember]
        public static Vector2Serialize Zero => new Vector2Serialize(0, 0);
        
        [IgnoreMember]
        public static Vector2Serialize One => new Vector2Serialize(0, 0);

        public bool Equals(Vector2Serialize other)
            => X.Equals(other.X) && Y.Equals(other.Y);

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
        
        public static Vector2Serialize operator -(Vector2Serialize left, Vector2Serialize right)
        {
            return new Vector2Serialize(left.X - right.X, left.Y - right.Y);
        }
        public static Vector2Serialize operator +(Vector2Serialize left, Vector2Serialize right)
        {
            return new Vector2Serialize(left.X + right.X, left.Y + right.Y);
        }
        public static Vector2Serialize operator *(Vector2Serialize source, float value)
        {
            return new Vector2Serialize(source.X * value, source.Y * value);
        }
        public static Vector2Serialize operator /(Vector2Serialize source, float value)
        {
            return new Vector2Serialize(source.X / value, source.Y / value);
        }
    }
}