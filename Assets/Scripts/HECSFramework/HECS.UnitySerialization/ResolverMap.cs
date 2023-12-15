using System;
using Components;
using HECSFramework.Unity;
using MessagePack.Resolvers;
using MessagePack;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HECSFramework.Core
{
    public partial class ResolversMap
    {
        public async Task<Entity> GetEntityFromResolver(EntityResolver entityResolver, bool needInitFromContainer, bool needForceAdd = false, int worldIndex = 0)
        {
            var unpack = new UnPackEntityResolver(entityResolver);
            var actorID = unpack.Components.FirstOrDefault(x => x is ActorContainerID containerID);

            if (actorID == null)
                return null;

            var container = actorID as ActorContainerID;

            Entity entity;
            entity = Entity.Get(EntityManager.Worlds[worldIndex], (actorID as ActorContainerID).ID);

            if (needInitFromContainer)
            {
                var loaded = await Addressables.LoadAssetAsync<ScriptableObject>(container.ID).Task;
                var loadedContainer = loaded as EntityContainer;
                loadedContainer.Init(entity);
                entity.LoadEntityFromResolver(entityResolver, needForceAdd);
            }

            entity.SetGuid(entityResolver.Guid);
            return entity;
        }
        
        public T DeserializeScriptableObject<T>(byte[] data)
        {
            var span = new ReadOnlySpan<byte>(data, 0, 4);

            var intKey = BitConverter.ToInt32(span);

            if (typeCodeToCustomResolver.TryGetValue(intKey, out var resolver))
                return resolver.DeserializeScriptableObject<T>(data);

            throw new Exception($"we dont have needed resolver for {intKey}, mby u forgot run codogen?");
        }
    }
}
