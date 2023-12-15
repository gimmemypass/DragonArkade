using System;
using System.Buffers;

namespace HECSFramework.Serialize
{
    /// <summary>
    /// we should release poolarray after using
    /// </summary>
    public ref struct PooledArray
    {
        public int Length;
        public byte[] Data;
        public bool IsValid;

        public void Release()
        {
            ArrayPool<byte>.Shared.Return(Data, true);
        }

        public static PooledArray GetPooledArray(int arraySize)
        {
            var data = new PooledArray();

            data.Length = arraySize;
            data.Data = ArrayPool<byte>.Shared.Rent(arraySize);
            data.IsValid = true;
            return data;
        }

        public static PooledArray GetPooledArrayCopyFromBuffer(ArrayBufferWriter<byte> arrayBufferWriter, int arraySize = 0)
        {
            var data = new PooledArray();
            var span = arrayBufferWriter.WrittenSpan;
            data.Length = span.Length;

            if (arraySize == 0 && data.Length < 2048)
                data.Data = ArrayPool<byte>.Shared.Rent(2048);
            else if (data.Length < arraySize)
                data.Data = ArrayPool<byte>.Shared.Rent(arraySize);
            else
                data.Data = ArrayPool<byte>.Shared.Rent(data.Length);

            for (int i = 0; i < span.Length; i++)
                data.Data[i] = span[i];

            data.IsValid = true;
            return data;
        }

        /// <summary>
        /// we release here pooled array and return its copy
        /// </summary>
        /// <returns>new array with copy of data</returns>
        public byte[] ToArray()
        {
            var newArray = new byte[Length];
            Array.Copy(Data, newArray, Length);
            Release();
            return newArray;
        }
    }
}