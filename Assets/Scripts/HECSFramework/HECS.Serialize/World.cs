using System;

namespace HECSFramework.Core
{
    public sealed partial class World
    {
        /// <summary>
        /// this method return resolver and remove entity from this world
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EntityResolver TakeEntityFromWorld(Entity entity)
        {
            var resolver = new EntityResolver().GetEntityResolver(entity);
            entity.Dispose();

            return resolver;
        }

        /// <summary>
        /// this method return resolver and remove entity from this world
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EntityResolver TakeEntityFromWorld(Guid guid)
        {
            if (TryGetEntityByID(guid, out var entity))
            {
                var resolver = new EntityResolver().GetEntityResolver(entity);
                entity.Dispose();

                return resolver;
            }

            HECSDebug.LogError("we dont have needed entity " + guid);
            return default;
        }

        /// <summary>
        /// This method return entity in world "to" and remove entity from world "from"
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Entity MirgrateEntity(Entity entity, World from, World to)
        {
            to.MigrateEntityToWorld(entity);
            return entity;
        }

        /// <summary>
        /// This method return entity in world "to" and remove entity from world "from"
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Entity MirgrateEntity(Guid entity, World from, World to)
        {
            var resolver = from.TakeEntityFromWorld(entity);
            var newEntity = resolver.GetEntityFromResolver(to.Index);
            return newEntity;
        }
    }
}