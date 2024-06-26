using System;
using HECSFramework.Serialize;
using UnityEngine;

namespace HECSFramework.Core 
{
    public abstract partial class CustomResolverProviderBase
    {
        public abstract T DeserializeScriptableObject<T>(byte[] data);
    }
    public partial class CustomResolverProvider<TypeToResolve, Resolver> : CustomResolverProviderBase where Resolver : IResolver<Resolver, TypeToResolve>, new() where TypeToResolve : new()
    {
        public override T DeserializeScriptableObject<T>(byte[] data)
        {
            var memory = new ReadOnlyMemory<byte>(data, 4, data.Length - 4);
            var resolver = MessagePack.MessagePackSerializer.Deserialize<Resolver>(memory);
            var needed = ScriptableObject.CreateInstance(typeof(TypeToResolve));
            if (needed is TypeToResolve casted)
            {
                resolver.Out(ref casted);

                if (needed is T result)
                    return result;
            }

            HECSDebug.LogError($"we cant cast {typeof(TypeToResolve).Name} to {typeof(T).Name}");
            return default;
        }
    }
}