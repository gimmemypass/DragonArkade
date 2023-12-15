namespace HECSFramework.Core
{
    public interface IAfterSerializationComponent
    {
        void AfterSync();
    }

    public interface IAfterInitSync 
    {
        void AfterInitSync();
    }
}