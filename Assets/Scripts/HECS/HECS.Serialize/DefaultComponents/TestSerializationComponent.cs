using System;
using System.Collections.Generic;
using HECSFramework.Core;
using HECSFramework.Serialize;
using MessagePack;

namespace Components
{
    [Serializable]
    [Documentation(Doc.HECS, Doc.Test, "test component for all kind of serialization here")]
    public sealed partial class TestSerializationComponent : BaseComponent, IAfterSerializationComponent, IBeforeSerializationComponent
    {
        [Field(0)]
        private int privateField;

        private int privatePartialCheck;

        [Field(1)]
        public int[] Array = new int[0];

        [Field(2)]
        public List<int> Collection = new List<int>();

        public int partialCheck;

        [Field(5, typeof(CheckResolver))]
        public int publicResolverCheck;

        public TestSerializationComponent()
        {
        }

        public TestSerializationComponent(int privateField, int privatePartialCheck, int[] array, List<int> collection, int partialCheck, int publicResolverCheck)
        {
            this.privateField = privateField;
            this.privatePartialCheck = privatePartialCheck;
            Array = array;
            Collection = collection;
            this.partialCheck = partialCheck;
            this.publicResolverCheck = publicResolverCheck;
        }

        public void BeforeSync()
        {
        }

        public void AfterSync()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is TestSerializationComponent component &&
                   privateField == component.privateField &&
                   privatePartialCheck == component.privatePartialCheck &&
                   EqualityComparer<int[]>.Default.Equals(Array, component.Array) &&
                   EqualityComparer<List<int>>.Default.Equals(Collection, component.Collection) &&
                   partialCheck == component.partialCheck &&
                   publicResolverCheck == component.publicResolverCheck;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(privateField, privatePartialCheck, Array, Collection, partialCheck, publicResolverCheck);
        }
    }

    [PartialSerializeField(3, "privatePartialCheck", "CheckResolver")]
    [PartialSerializeField(4, "partialCheck")]
    public sealed partial class TestSerializationComponent
    {

    }

    [MessagePackObject]
    public struct CheckResolver : IResolver<CheckResolver, int>
    {
        [Key(0)]
        public int Data;

        public CheckResolver In(ref int data)
        {
            Data = data;
            return this;
        }

        public void Out(ref int data)
        {
            data = Data;
        }
    }
}