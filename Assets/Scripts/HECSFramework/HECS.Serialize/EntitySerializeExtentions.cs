using Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HECSFramework.Core
{
    public static class EntitySerializeExtentions
    {
        public static Entity CopyEntity(this Entity entity)
        {
            var save = new EntityResolver().GetEntityResolver(entity);
            var copy = Entity.Get(entity.World, entity.ID);
            var unpack = new UnPackEntityResolver(save);
            unpack.Init(copy);
            return copy;
        }

        public static Entity CopyEntity(this Entity entity, World world)
        {
            var save = new EntityResolver().GetEntityResolver(entity);
            var copy = Entity.Get(world, entity.ID);
            var unpack = new UnPackEntityResolver(save);
            unpack.Init(copy);
            return copy;
        }

        public static void LoadEntityFromResolver(this Entity entity, EntityResolver entityResolver, bool forceAdd = true)
        {
            foreach (var c in entityResolver.Components)
            {
                var componentResolver = c;
                EntityManager.ResolversMap.LoadComponentFromContainer(ref componentResolver, ref entity, forceAdd);
            }

            if (forceAdd)
            {
                foreach (var s in entityResolver.Systems)
                {
                    var newSys = EntityManager.ResolversMap.GetSystemFromContainer(s);

                    if (newSys == null)
                    {
                        HECSDebug.LogWarning("we have null system at " + entityResolver.Guid);
                        continue;
                    }

                    if (entity.Systems.Any(x => x.GetTypeHashCode == newSys.GetTypeHashCode))
                        continue;

                    entity.AddHecsSystem(newSys);
                }
            }

            entity.SetGuid(entityResolver.Guid);
        }

        public static Entity GetEntityFromResolver(this EntityResolver entityResolver, int worldIndex = 0, bool addComponent = true)
        {
            var data = new UnPackEntityResolver(entityResolver);
            var id = data.Components.FirstOrDefault(x => x is ActorContainerID) as ActorContainerID;

            Entity entity;

            if (id != null)
                entity = Entity.Get(EntityManager.Worlds[worldIndex], id.ID);
            else
                entity = Entity.Get(EntityManager.Worlds[worldIndex], entityResolver.Guid.ToString());

            data.Init(entity);
            entity.SetGuid(entityResolver.Guid);
            return entity;
        }

        public static List<EntityResolver> GetResolversFromEntitiesList(this List<Entity> entities)
        {
            var list = new List<EntityResolver>(8);

            foreach (var entity in entities)
                list.Add(new EntityResolver().GetEntityResolver(entity));

            return list;
        }

        public static void LoadEntityFromResolver(this Entity entity, EntityResolver entityResolver)
        {
            entity.LoadEntityFromResolver(entityResolver, true);
        }

        public static List<Entity> GetEntitiesFromResolvers(this List<EntityResolver> entitiesResolvers, int worldIndex = 0)
        {
            var list = new List<Entity>(entitiesResolvers.Count);

            foreach (var e in entitiesResolvers)
                list.Add(EntityManager.ResolversMap.GetEntityFromResolver(e, worldIndex));

            return list;
        }

        public static List<EntityResolver> GetEntityResolvers(this List<Entity> entities)
        {
            var list = new List<EntityResolver>(entities.Count);

            foreach (var e in entities)
                list.Add(new EntityResolver().GetEntityResolver(e));

            return list;
        }

        public static void GetEntityResolvers(this List<Entity> entities, ref List<EntityResolver> list)
        {
            list.Clear();

            foreach (var e in entities)
                list.Add(new EntityResolver().GetEntityResolver(e));
        }

        public static Entity GetEntity(this UnPackEntityResolver unpackedEntityResolver, World world)
        {
            unpackedEntityResolver.TryGetComponent<ActorContainerID>(out var id);
            var copy = Entity.Get(world, id.ID);
            unpackedEntityResolver.Init(copy);
            return copy.CopyEntity();
        }

        /// <summary>
        /// Get Entity from core container
        /// </summary>
        /// <param name="entityCoreContainer">Core contaner</param>
        /// <param name="entityName">Just name for understanding what kind of entity is it, if u dont assign any value, we take name from container </param>
        /// <param name="worldIndex">u should provide index of world</param>
        /// <returns></returns>
        public static Entity GetEntityFromCoreContainer(this EntityCoreContainer entityCoreContainer, int worldIndex = 0, string entityName = default)
        {
            Entity entity = null;

            if (entityCoreContainer == null)
            {
                HECSDebug.LogError("container is null");
                return null;
            }

            if (string.IsNullOrEmpty(entityName))
                entity = Entity.Get(EntityManager.Worlds[worldIndex], entityName);
            else
                entity = Entity.Get(EntityManager.Worlds[worldIndex], entityCoreContainer.ContainerID);

            entityCoreContainer.Init(entity);
            return entity;
        }

        public static Entity GetEntityFromCoreContainer(this IEntityContainer entityCoreContainer, int worldIndex = 0, string entityName = default)
        {
            Entity entity = null;

            if (entityCoreContainer == null)
            {
                HECSDebug.LogError("container is null");
                return null;
            }

            if (string.IsNullOrEmpty(entityName))
                entity = Entity.Get(EntityManager.Worlds[worldIndex], entityName);
            else
                entity = Entity.Get(EntityManager.Worlds[worldIndex], entityCoreContainer.ContainerID);

            entityCoreContainer.Init(entity);
            return entity;
        }

        public static Entity GetEntityFromCoreContainer(this IEntityContainer entityCoreContainer, World world, string entityName = default)
        {
            Entity entity = null;

            if (entityCoreContainer == null)
            {
                HECSDebug.LogError("container is null");
                return null;
            }

            if (string.IsNullOrEmpty(entityName))
                entity = Entity.Get(world, entityName);
            else
                entity = Entity.Get(world, entityCoreContainer.ContainerID);

            entityCoreContainer.Init(entity);
            
            return entity;
        }
    }
}