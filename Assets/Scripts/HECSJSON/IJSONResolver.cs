using HECSFramework.Core;

namespace HECSFramework.Core
{
    public interface IJSONResolver { }

    public interface IJSONResolver<TypeToResolve, Resolver> : IJSONResolver
    {
        Resolver In(ref TypeToResolve data);
        void Out(ref TypeToResolve data);
    }
}

/// <summary>
/// this interface should be using for custom resolvers,
/// its just for consistent signature same as codogen 
/// </summary>
/// <typeparam name="T">ResolverType</typeparam>
/// <typeparam name="U">TypeForResolving</typeparam>
public interface ISaveJSONToResolver<T> where T : IJSONResolver
{
    void SaveToJSONResolver(ref T resolver);
}

public interface ILoadFromJSONResolver<T> where T : IJSONResolver
{
    void LoadFromJSONResolver(ref T resolver);
}