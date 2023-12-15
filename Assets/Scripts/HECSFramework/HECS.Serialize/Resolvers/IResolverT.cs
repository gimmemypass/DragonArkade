public interface IResolver<T> : IResolver
{
    void Out(ref T data);
}