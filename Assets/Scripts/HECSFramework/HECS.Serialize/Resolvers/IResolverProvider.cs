public interface IResolverProvider
{
}

public interface IResolverProvider<T, U>  : IResolverProvider where T: IResolver<U>
{
    T GetDataContainer(U data); 
}