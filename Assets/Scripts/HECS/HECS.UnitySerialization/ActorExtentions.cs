using System;
using System.Threading.Tasks;
using Components;
using HECSFramework.Core;
using UnityEngine.AddressableAssets;

namespace HECSFramework.Unity
{
    public static partial class ActorExtentions
    {
        public static async Task<Actor> GetActor(this Entity entity, Action<Actor> callBack = null)
        {
            if (entity == null)
                throw new Exception("entity == null");

            var save = new EntityResolver().GetEntityResolver(entity);

            if (entity.TryGetComponent(out ActorContainerID container))
            {
                var actorContainer = await Addressables.LoadAssetAsync<EntityContainer>(container.ID).Task;
                var actorPrfb = await actorContainer.GetActor();
                callBack?.Invoke(actorPrfb);
                return actorPrfb;
            }

            throw new Exception("All going to second anus, because we dont have actor container id " + entity.ID);
        }
    }
}