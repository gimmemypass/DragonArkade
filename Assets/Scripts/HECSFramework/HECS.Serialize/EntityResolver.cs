using MessagePack;
using System;
using System.Collections.Generic;

namespace HECSFramework.Core
{
    [MessagePackObject, Serializable]
    public partial struct EntityResolver
    {
        [Key(0)]
        public List<ResolverDataContainer> Systems;

        [Key(1)]
        public List<ResolverDataContainer> Components;

        [Key(2)]
        public Guid Guid;

        public EntityResolver GetEntityResolver(Entity entity)
        {
            Systems = new List<ResolverDataContainer>(32);
            Components = new List<ResolverDataContainer>(32);
            Guid = entity.GUID;

            foreach (var c in entity.GetComponentsByType<IComponent>())
            {
                if (c == null)
                    continue;

                if (c is INotCore)
                    continue;

                Components.Add(EntityManager.ResolversMap.GetComponentContainer(c));
            }

            foreach (var s in entity.Systems)
            {
                if (s == null)
                    continue;
                
                if (s is INotCore)
                    continue;

                Systems.Add(EntityManager.ResolversMap.GetSystemContainer(s));
            }

            return this;
        }
    }
}
