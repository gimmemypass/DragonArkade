using System;
using System.Collections.Generic;
using HECSFramework.Core;
using MessagePack;

namespace Components
{
    public partial class PredicatesComponent : IBeforeSerializationComponent, IAfterSerializationComponent
    {
        [HideInInspectorCrossPlatform]
        [Field(0)] public byte[] savePredicates;

        public void AfterSync()
        {
            Predicates = MessagePackSerializer.Deserialize<List<IPredicate>>(savePredicates);
        }

        public void BeforeSync()
        {
            InitBeforeSync();
            savePredicates = MessagePackSerializer.Serialize(Predicates);
        }

        partial void InitBeforeSync();
    }

    [MessagePackObject]
    public class FastPredicateResolver
    {
        [Key(0)]
        public byte[] data;

        [Key(1)]
        public Type Type;

        public void SavePredicate<T>(T predicate) where T : IPredicate
        {
            Type = predicate.GetType();
            data = MessagePack.MessagePackSerializer.Serialize(predicate);
        }

        public IPredicate GetPredicate()
        {
            var t = (IPredicate)Activator.CreateInstance(Type);
            return GetPredicate(t);
        }

        private IPredicate GetPredicate<T>(T predicate) where T : IPredicate
        {
            return MessagePack.MessagePackSerializer.Deserialize<T>(data);
        }
    }
}