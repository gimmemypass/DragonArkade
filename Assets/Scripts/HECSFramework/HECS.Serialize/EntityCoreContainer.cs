using Components;
using System.Collections.Generic;

namespace HECSFramework.Core
{
    public abstract class EntityCoreContainer : IEntityContainer
    {
        public abstract string ContainerID { get; }
        public abstract List<IComponent> GetComponents();
        public abstract List<ISystem> GetSystems();

        public virtual void Init(Entity entityForInit)
        {
            //todo тут скорее всего нужно не через резолвер и ентити, а через сериализацию/резолвеоы компонентов накатывать
            var entity = Entity.Get(ContainerID);
            entity.AddComponent(new ActorContainerID { ID = ContainerID });

            var components = GetComponents();
            var systems = GetSystems();

            foreach (var component in components)
            {
                if (component == null)
                    continue;

                entity.AddComponent(component);
            }

            foreach (var system in systems)
            {
                if (system == null)
                    continue;

                entity.AddHecsSystem(system);
            }

            var resolver = new EntityResolver().GetEntityResolver(entity);
            entityForInit.LoadEntityFromResolver(resolver);
        }
    }
}