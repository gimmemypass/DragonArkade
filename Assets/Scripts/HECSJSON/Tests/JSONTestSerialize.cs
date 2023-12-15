using HECSFramework.Core;
using Newtonsoft.Json;

namespace HECSFramework.Serialize
{
    [JSONHECSSerialize]
    public partial class JSONTestSerialize : JSONTestSerializeParent, IAfterSerializationComponent
    {
        [Field(0)]
        public int Data;

        [Field(1)]
        public string Check;

        public void AfterSync()
        {
            throw new System.NotImplementedException();
        }
    }

    public class JSONTestSerializeParent : IBeforeSerializationComponent
    {
        [Field(3)]
        public int Data2;

        public void BeforeSync()
        {
            throw new System.NotImplementedException();
        }
    }

    public partial class JSONTestSerialize : JSONTestSerializeParent
    {
        [Field(4)]
        public int PartialData;

        [Field(5)]
        private string Common;

        [Field(6)]
        [JSONHECSFieldByResolver(typeof(CheckThrow))]
        public CheckThrowResolver CheckThrowResolver;

        [Field(7)]
        [JSONHECSFieldByResolver(typeof(TestPrivateResolver))]
        private int CheckData;
    }

    [JSONHECSSerialize]
    public class CheckThrowResolver 
    {
        [Field(0)]
        public int T1;
        [Field(1)]
        public int T2;
    }

    public struct TestPrivateResolver : IJSONResolver<int, TestPrivateResolver>
    {
        public int Data;

        public TestPrivateResolver In(ref int data)
        {
            Data = data;
            return this;
        }

        public void Out(ref int data)
        {
            data = Data;
        }
    }


    [JsonObject]
    public struct CheckThrow : IJSONResolver<CheckThrowResolver, CheckThrow>
    {
        [JsonProperty("TT1")]
        public int TT1;

        [JsonProperty("TT2")]
        public int TT2;

        public CheckThrow In(ref CheckThrowResolver data)
        {
            TT1 = data.T1;
            TT2 = data.T2;
            return this;
        }

        public void Out(ref CheckThrowResolver data)
        {
            data.T1 = TT1;
            data.T2 = TT2;
        }
    }
}