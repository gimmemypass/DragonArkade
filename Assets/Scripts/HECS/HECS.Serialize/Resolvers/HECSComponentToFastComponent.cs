using System;
using System.Collections.Generic;
using HECSFramework.Core;

namespace HECSFramework.Serialize
{
    public static partial class HECSComponentToFastComponent  
    {
        private readonly static Dictionary<Type, IAddComponentToFastEntity> componentToResolvers;

        public static void AddComponentToFastEntity(IComponent component, FastEntity fastEntity)
        {
            var key = component.GetType();

            if (componentToResolvers.TryGetValue(key, out var addComponentToFastEntity))
            {
                addComponentToFastEntity.AddComponentToFastEntity(component, fastEntity);
            }
        }
    }

    public sealed class ComponentToResolver<Component, Resolver> : IAddComponentToFastEntity where Component: IComponent where Resolver: struct, IFastComponent, IResolver<Resolver, Component>
    {
        private Resolver resolver = default;

        public void AddComponentToFastEntity(IComponent component, FastEntity fastEntity)
        {
            if (component is Component needed)
            {
                resolver.In(ref needed);
                fastEntity.AddComponent(resolver);
            }
        }
    }

    public interface IAddComponentToFastEntity
    {
        void AddComponentToFastEntity(IComponent component, FastEntity fastEntity);
    }
}
