namespace HECSFramework.Core
{
    public interface IEntityContainer
    {
        public void Init(Entity entity);
        public string ContainerID { get; }
    }
}