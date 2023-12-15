using System;
using System.Buffers;
using System.Collections.Generic;
using MessagePack;

public partial interface IData { }

namespace HECSFramework.Core
{
    public delegate void ProcessResolverContainer(ref ResolverDataContainer dataContainerForResolving, ref Entity entity);
    public delegate void ProcessReadOnlyMemoryToComponent(ref ReadOnlyMemory<byte> dataContainerForResolving, int typeHashCode, Entity entity);
    
    public delegate ResolverDataContainer GetContainer<T>(T component) where T : IComponent;
    public delegate IComponent GetComponentFromMemory (ref ReadOnlyMemory<byte> dataContainerForResolving, int componentTypeCode);

    public partial class ResolversMap
    {
        private GetContainer<IComponent> GetComponentContainerFunc;
        private Dictionary<Type, IResolverProvider> resolverProviders;

        /// <summary>
        /// Factory resolver data containers to components
        /// </summary>
        public Func<ResolverDataContainer, IComponent> GetComponentFromContainer { get; private set; }
        
        /// <summary>
        /// Its for resolving container when we alrdy have entity
        /// </summary>
        public ProcessResolverContainer ProcessResolverContainer { get; private set; }

        public void LoadDataFromContainer(ref ResolverDataContainer dataContainerForResolving, int worldIndex = 0) => LoadDataFromContainerSwitch(dataContainerForResolving, worldIndex);

        public void LoadComponentFromContainer(ref ResolverDataContainer resolverDataContainer, ref Entity entity, bool checkForAvailable = false)
        {
            if (checkForAvailable)
            {
                    if (!entity.ContainsMask(resolverDataContainer.TypeHashCode) && TypesMap.ContainsComponent(resolverDataContainer.TypeHashCode))
                    {
                        var component = GetComponentFromContainer(resolverDataContainer);
                        entity.AddComponent(component);
                    }
            }

            EntityManager.ResolversMap.ProcessResolverContainer(ref resolverDataContainer, ref entity);
        }

        public ResolverDataContainer GetSystemContainer<T>(T system) where T: ISystem
        {
            return new ResolverDataContainer { Data = null, EntityGuid = system.Owner.GUID, Type = 1, TypeHashCode = system.GetTypeHashCode };
        }

        public T GetResolver<T,U>(U obj) where T: IResolver<U>
        {
            if (obj == null)
            {
                HECSDebug.LogError("null object on argument");
                return default;
            }

            var key = typeof (T);
            if (resolverProviders.TryGetValue(key, out var provider))
            {
                var providerCast = provider as IResolverProvider<T, U>;
                return providerCast.GetDataContainer(obj);
            }
            else
            {
                HECSDebug.LogError("we dont have resolver provider for " + key.Name + " check codogen first");
                return default;
            }
        }

        public ISystem GetSystemFromContainer(ResolverDataContainer resolverDataContainer)
        {
            return TypesMap.GetSystemFromFactory(resolverDataContainer.TypeHashCode);
        }

        public ResolverDataContainer GetComponentContainer<T>(T component) where T : IComponent => GetComponentContainerFunc(component);

        partial void LoadDataFromContainerSwitch(ResolverDataContainer dataContainerForResolving, int worldIndex);

        public Entity GetEntityFromResolver(EntityResolver entityResolver, int worldIndex = 0) => entityResolver.GetEntityFromResolver(worldIndex);
        
        public void CopyFromComponentToEntityComponent(IComponent from, Entity to, bool checkForAvailability = false)
        {
            var container = GetComponentContainer(from);
            var data = MessagePackSerializer.Serialize(container);
            var unpack = MessagePackSerializer.Deserialize<ResolverDataContainer>(data);
            LoadComponentFromContainer(ref unpack, ref to, checkForAvailability);
        }

        private ResolverDataContainer PackComponentToContainer<T, U>(T component, U data) where T : IComponent where U : IData
        {
            return new ResolverDataContainer
            {
                Data = MessagePackSerializer.Serialize(data),
                EntityGuid = component.Owner != null ? component.Owner.GUID : default,
                Type = 0,
                TypeHashCode = component.GetTypeHashCode,
            };
        }

        partial void InitPartialCommandResolvers();
    }

    [MessagePackObject, Serializable]
    public struct ResolverDataContainer : IData
    {
        /// <summary>
        /// 0 - Component, 1  - System, 2- Command
        /// </summary>
        [Key(0)]
        public int Type;
        
        [Key(1)]
        public int TypeHashCode;
        
        [Key(2)]
        public byte[] Data;
        
        [Key(3)]
        public Guid EntityGuid;

        [Key(4)]
        public bool IsSyncSelf;
    }
}