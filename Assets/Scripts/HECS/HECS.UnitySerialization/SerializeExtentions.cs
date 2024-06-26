using System.Threading.Tasks;
using HECSFramework.Core;

namespace HECSFramework.Unity.Helpers
{
    public static partial class SerializeExtentions
    {
        public static async ValueTask<Actor> GetActorFromResolver(this EntityResolver entityResolver, bool needForceAdd = true, int worldIndex = 0)
        {
            var actor  = await entityResolver.GetActorFromResolver(worldIndex: worldIndex);
            return actor;
        }

        public static EntityResolver GetEntityResolver(this EntityContainer entityContainer)
        {
            var unpack = new UnpackContainer(entityContainer);
            var data = new EntityResolver();

            data.Components = new System.Collections.Generic.List<ResolverDataContainer>();
            data.Systems = new System.Collections.Generic.List<ResolverDataContainer>();

            foreach (var c in unpack.Components)
                data.Components.Add(EntityManager.ResolversMap.GetComponentContainer(c));

            foreach (var s in unpack.Systems)
                data.Systems.Add(EntityManager.ResolversMap.GetSystemContainer(s));

            return data;
        }
    }
}