using System;
using System.Collections.Generic;
using HECSFramework.Serialize;
using Strategies;

namespace HECSFramework.Core
{
    public partial class ResolversMap
    {
        private Dictionary<Type, CustomResolverProviderBase> typeToCustomResolver;
        private Dictionary<int, CustomResolverProviderBase> typeCodeToCustomResolver;
        private Dictionary<int, Type> getTypeIndexToType;

        public byte[] SerializeCustom<T>(T data)
        {
            var key = data.GetType();

            if (typeToCustomResolver.TryGetValue(key, out var resolverProvider))
                return resolverProvider.Serialize(data);

            throw new Exception($"we dont have needed resolver for {key.Name}, mby u forgot run codogen?");
        }

        public Type GetTypeByIndex(int index)
        {
            return getTypeIndexToType[index];
        }

        public T Deserialize<T>(byte[] data)
        {
            var span = new ReadOnlySpan<byte>(data, 0, 4);

            var intKey = BitConverter.ToInt32(span);

            if (typeCodeToCustomResolver.TryGetValue(intKey, out var resolver))
                return resolver.Deserialize<T>(data);


            throw new Exception($"we dont have needed resolver for {intKey}, mby u forgot run codogen?");
        }

        public void DeserializeTo<T>(T obj, byte[] data)
        {
            var span = new ReadOnlySpan<byte>(data, 0, 4);

            var intKey = BitConverter.ToInt32(span);

            if (typeCodeToCustomResolver.TryGetValue(intKey, out var resolver))
                resolver.DeserializeTo<T>(obj, data);


            throw new Exception($"we dont have needed resolver for {intKey}, mby u forgot run codogen?");
        }
    }

    public partial class CustomResolverProvider<TypeToResolve, Resolver> : CustomResolverProviderBase where Resolver : IResolver<Resolver, TypeToResolve>, new() where TypeToResolve : new()
    {
        public readonly int TypeCode = IndexGenerator.GetIndexForType(typeof(TypeToResolve));

        //we return here new object of T
        public override T Deserialize<T>(byte[] data)
        {
            var memory = new ReadOnlyMemory<byte>(data, 4, data.Length - 4);
            var resolver = MessagePack.MessagePackSerializer.Deserialize<Resolver>(memory);
            var needed = new TypeToResolve();
            resolver.Out(ref needed);

            if (needed is T result)
                return result;


            HECSDebug.LogError($"we cant cast {typeof(TypeToResolve).Name} to {typeof(T).Name}");
            return default;
        }

        public override void DeserializeTo<T>(T obj, byte[] data)
        {
            if (obj == null)
            {
                HECSDebug.LogError("object is null");
                return;
            }

            var memory = new ReadOnlyMemory<byte>(data, 4, data.Length - 4);
            var resolver = MessagePack.MessagePackSerializer.Deserialize<Resolver>(memory);

            if (obj is TypeToResolve result)
            {
                resolver.Out(ref result);
                return;
            }

            HECSDebug.LogError($"we cant cast {typeof(TypeToResolve).Name} to {typeof(T).Name}");
        }

        //we return here new array, if we need non alloc realization, we should add new method with buffer 
        public override byte[] Serialize<T>(T data)
        {
            if (data is TypeToResolve needed)
            {
                var poolBuffer = new PooledBuffer();
                var buffer = poolBuffer.GetBufferWriter();
                var span = buffer.GetSpan(4);

                if (BitConverter.TryWriteBytes(span, TypeCode))
                {
                    buffer.Advance(4);
                    var resolver = new Resolver();
                    resolver.In(ref needed);
                    MessagePack.MessagePackSerializer.Serialize(buffer, resolver);

                    var result = buffer.WrittenSpan.ToArray();
                    poolBuffer.Release();
                    return result;
                }
            }

            HECSDebug.LogError($"wrong serialization {typeof(TypeToResolve).Name} to {typeof(T).Name}");
            return default;
        }
    }

    public abstract partial class CustomResolverProviderBase
    {
        public abstract byte[] Serialize<T>(T data);

        public abstract T Deserialize<T>(byte[] data);
        public abstract void DeserializeTo<T>(T obj, byte[] data);
    }
}