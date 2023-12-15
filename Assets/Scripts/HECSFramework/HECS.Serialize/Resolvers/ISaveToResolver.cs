
/// <summary>
/// this interface should be using for custom resolvers,
/// its just for consistent signature same as codogen 
/// </summary>
/// <typeparam name="T">ResolverType</typeparam>
/// <typeparam name="U">TypeForResolving</typeparam>
public interface ISaveToResolver<T> where T : IResolver
{
    void Save(ref T resolver);
}

public interface ILoadFromResolver<T> where T : IResolver
{
    void Load(ref T resolver);
}