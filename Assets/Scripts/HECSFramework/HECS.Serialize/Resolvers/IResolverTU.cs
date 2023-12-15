
namespace HECSFramework.Serialize
{
    /// <summary>
    /// this interface should be using for custom resolvers,
    /// its just for consistent signature same as codogen 
    /// </summary>
    /// <typeparam name="Resolver">ResolverType</typeparam>
    /// <typeparam name="Data">TypeForResolving</typeparam>
    public interface IResolver<Resolver, Data> 
    {
        Resolver In(ref Data data);
        void Out(ref Data data);
    }
}