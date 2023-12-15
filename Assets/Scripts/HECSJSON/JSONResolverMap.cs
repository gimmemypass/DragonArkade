using System;
using System.Collections.Generic;
using Components;
using HECSFramework.Serialize;
using Newtonsoft.Json;

namespace HECSFramework.Core
{
    [Documentation(Doc.Serialization, Doc.JSONSerialization, "we also have JSON Serialization part, purpose - json serialization for databases and online services")]
    public partial class ResolversMap
    {
        private Dictionary<Type, JSONSerializationProvider> typeToJSONResolver;
        private Dictionary<int, JSONSerializationProvider> typeCodeToJSONResolver;
        private Dictionary<int, Type> getTypeIndexToTypeJSON;
        private Dictionary<Type, int> typeToIndexJSON;

        /// <summary>
        /// here we return only resolver for actual type, without context info like typeIndex
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetJSONResolver<T>(T data)
        {
            var key = data.GetType();

            if (typeToJSONResolver.TryGetValue(key, out var provider))
            {
                return provider.SerializeToJSON(data);
            }

            return String.Empty;
        }

        public void DeserializeJSONContainerToObject<T>(T value, JSONContainer container)
        {
            if (typeCodeToJSONResolver.TryGetValue(container.TypeIndex, out var provider))
            {
                provider.DeserializeJSONTo(value, container.JSON);
            }
        }

        public T GetDeserializedOBjectFromJSONContainer<T>(JSONContainer container)
        {
            if (typeCodeToJSONResolver.TryGetValue(container.TypeIndex, out var provider))
            {
                return provider.JSONDeserialize<T>(container.JSON);
            }

            return default;
        }

        /// <summary>
        /// here we return resolver inside container with context (typeindex)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetJSONResolverContainer<T>(T data)
        {
            var key = data.GetType();

            if (typeToJSONResolver.TryGetValue(key, out var provider))
            {
                var resolver = provider.SerializeToJSON(data);
                var container = new JSONContainer { JSON = resolver, TypeIndex = typeToIndexJSON[key] };

                var json = JsonConvert.SerializeObject(container);
                return json;
            }

            return String.Empty;
        }

        public JSONEntityContainer GetEntityContainerJSON(Entity entity)
        {
            var container = new JSONEntityContainer();

            foreach (var c in entity.Components)
            {
                var component = entity.GetComponent(c);
                var jsonComponent = GetJSONResolverContainer(component);

                if (string.IsNullOrEmpty(jsonComponent))
                    container.Components.Add(jsonComponent);
            }

            foreach (var s in entity.Systems)
            {
                var json = GetJSONResolverContainer(s);

                if (string.IsNullOrEmpty(json))
                    container.Components.Add(json);
            }

            return container;
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class JSONEntityContainer
    {
        [JsonProperty("Components")]
        public List<string> Components = new List<string>();

        [JsonProperty("Systems")]
        public List<string> Systems = new List<string>();

        public void DeserializeToEntity(Entity entity, bool addComponentsAndSystem = true)
        {
            foreach (var c in Components)
            {
                var resolverContainer = JsonConvert.DeserializeObject<JSONContainer>(c);

                if (entity.ContainsMask(resolverContainer.TypeIndex))
                {
                    EntityManager.ResolversMap.DeserializeJSONContainerToObject(entity.GetComponent(resolverContainer.TypeIndex), resolverContainer);
                }
                else
                {
                    var component = EntityManager.ResolversMap.GetDeserializedOBjectFromJSONContainer<IComponent>(resolverContainer);
                    entity.AddComponent(component);
                }
            }

            foreach (var s in Systems)
            {
                var resolverContainer = JsonConvert.DeserializeObject<JSONContainer>(s);
                var system = EntityManager.ResolversMap.GetDeserializedOBjectFromJSONContainer<ISystem>(resolverContainer);
                entity.AddHecsSystem(system);
            }
        }

        public JSONEntityContainer SerializeEntity(Entity entity)
        {
            foreach (var c in entity.Components)
            {
                var component = entity.GetComponent(c);
                Components.Add(EntityManager.ResolversMap.GetJSONResolverContainer(component));
            }

            foreach (var s in entity.Systems)
            {
                Systems.Add(EntityManager.ResolversMap.GetJSONResolverContainer(s));
            }

            return this;
        }

        public JSONEntityContainer SerializeEntitySavebleOnly(Entity entity)
        {
            foreach (var c in entity.Components)
            {
                var component = entity.GetComponent(c);

                if (component is ISavebleComponent)
                {
                    Components.Add(EntityManager.ResolversMap.GetJSONResolverContainer(component));
                }
            }

            return this;
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class JSONContainer
    {
        [JsonProperty("TypeIndex")]
        public int TypeIndex;

        [JsonProperty("JSON")]
        public string JSON;
    }

    public abstract partial class JSONSerializationProvider
    {
        public abstract string SerializeToJSON<T>(T data);
        public abstract T JSONDeserialize<T>(string data);
        public abstract void DeserializeJSONTo<T>(T obj, string data);
    }

    public partial class JSONResolverProvider<TypeToResolve, Resolver> : JSONSerializationProvider where TypeToResolve : new() where Resolver : struct, IJSONResolver<TypeToResolve, Resolver>
    {
        public readonly int TypeCode = IndexGenerator.GetIndexForType(typeof(TypeToResolve));

        //we return here new object of T
        public override T JSONDeserialize<T>(string data)
        {
            var resolver = JsonConvert.DeserializeObject<Resolver>(data);
            var needed = new TypeToResolve();
            resolver.Out(ref needed);

            if (needed is T result)
                return result;


            HECSDebug.LogError($"we cant cast {typeof(TypeToResolve).Name} to {typeof(T).Name}");
            return default;
        }

        public override void DeserializeJSONTo<T>(T obj, string data)
        {
            if (obj == null)
            {
                HECSDebug.LogError("object is null");
                return;
            }

            var resolver = JsonConvert.DeserializeObject<Resolver>(data);

            if (obj is TypeToResolve result)
            {
                resolver.Out(ref result);
                return;
            }

            HECSDebug.LogError($"we cant cast {typeof(TypeToResolve).Name} to {typeof(T).Name}");
        }

        //we return here new array, if we need non alloc realization, we should add new method with buffer 
        public override string SerializeToJSON<T>(T data)
        {
            if (data is TypeToResolve needed)
            {
                var resolver = new Resolver();
                resolver.In(ref needed);
                var result = JsonConvert.SerializeObject(resolver);
                return result;
            }

            HECSDebug.LogError($"wrong serialization {typeof(TypeToResolve).Name} to {typeof(T).Name}");
            return default;
        }
    }
}