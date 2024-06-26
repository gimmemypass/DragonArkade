using System.Collections.Generic;
using System.Linq;
using Components;

namespace HECSFramework.Core
{
    public struct UnPackEntityResolver : IEntityContainer
    {
        public List<IComponent> Components;
        public List<ISystem> Systems;

        public string ContainerID
        {
            get
            {
                var needed = Components.FirstOrDefault(x=> x is ActorContainerID actorContainerID) as ActorContainerID;

                if (needed != null)
                    return needed.ID;

                return "Dont have id";
            }
        }

        public UnPackEntityResolver(EntityResolver entityResolver)
        {
            Components = new List<IComponent>(16);
            Systems = new List<ISystem>(6);
            var resolverMap = EntityManager.ResolversMap;

            foreach (var c in entityResolver.Components)
            {
                var component = EntityManager.ResolversMap.GetComponentFromContainer(c);
                if (component != null)
                {
                    Components.Add(component);
                }
            }

            foreach (var s in entityResolver.Systems)
            {
                var sLoaded = EntityManager.ResolversMap.GetSystemFromContainer(s);
                
                if (sLoaded != null)
                    Systems.Add(sLoaded);
            }
        }

        public bool TryGetComponent<T>(out T component) where T : IComponent
        {
            for (int i = 0; i < Components.Count; i++)
            {
                IComponent c = Components[i];
                if (c is T needed)
                {
                    component = needed;
                    return true;
                }
            }

            component = default;
            return false;
        }

        public void Init(Entity entity)
        {
            foreach (var c in Components)
            {
                if (c != null)
                    entity.AddComponent(c);
            }

            foreach (var s in Systems)
            {
                if (s != null)
                    entity.AddHecsSystem(s);
            }
        }
    }
}