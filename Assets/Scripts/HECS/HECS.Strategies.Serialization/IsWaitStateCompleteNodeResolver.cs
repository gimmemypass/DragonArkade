//using HECSFramework.Serialize;
//using MessagePack;
//using Strategies;

//namespace HECSFramework.Core
//{
//    [MessagePackObject()]
//    [HECSManualResolver(typeof(IsWaitStateCompleteNode))]
//    public struct IsWaitStateCompleteNodeResolver : IResolver<IsWaitStateCompleteNodeResolver, IsWaitStateCompleteNode>
//    {
//        public IsWaitStateCompleteNodeResolver In(ref IsWaitStateCompleteNode data)
//        {
//            return this;
//        }

//        public void Out(ref IsWaitStateCompleteNode data)
//        {
//        }
//    }
//}