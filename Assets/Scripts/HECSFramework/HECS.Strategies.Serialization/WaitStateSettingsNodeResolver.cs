//using HECSFramework.Serialize;
//using MessagePack;
//using Strategies;

//namespace HECSFramework.Core
//{
//    [MessagePackObject()]
//    [HECSManualResolver(typeof(WaitStateSettingsNode))]
//    public struct WaitStateSettingsNodeResolver : IResolver<WaitStateSettingsNodeResolver, WaitStateSettingsNode>
//    {
//        [Key(0)]
//        public float MinTime;
        
//        [Key(1)]
//        public float MaxTime;
//        public WaitStateSettingsNodeResolver In(ref WaitStateSettingsNode data)
//        {
//            MinTime = data.MinTime;
//            MaxTime = data.MaxTime;
//            return this;
//        }

//        public void Out(ref WaitStateSettingsNode data)
//        {
//            data.MaxTime = MaxTime;
//            data.MinTime = MinTime;
//        }
//    }
//}