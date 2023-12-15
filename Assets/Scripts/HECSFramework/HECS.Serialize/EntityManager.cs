namespace HECSFramework.Core
{
    public partial class EntityManager
    {
        private ResolversMap resolversMap = new ResolversMap();
        public static ResolversMap ResolversMap => Instance.resolversMap;
    }
}
