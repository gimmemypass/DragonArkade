//using System;
//using System.Collections.Generic;
//using HECSFramework.Core;
//using HECSFramework.Serialize;
//using MessagePack;
//using Strategies;

//namespace Components
//{

//    [MessagePackObject]
//    [HECSManualResolver(typeof(StateStack))]
//    public class StateStackResolver : IResolver<StateStackResolver, StateStack>
//    {
//        [Key(0)] public List<byte[]> Nodes;

//        public StateStackResolver In(ref StateStack data)
//        {
//            Nodes = new();
//            foreach (var node in data.Nodes)
//            {
//                var bytes = EntityManager.ResolversMap.SerializeCustom(node);
//                Nodes.Add(bytes);
//            }

//            return this;
//        }

//        public void Out(ref StateStack data)
//        {
//            var nodes = new List<LogNode>();
//            foreach (var bytes in Nodes)
//            {
//#if UNITY_EDITOR
//                var node = EntityManager.ResolversMap.DeserializeScriptableObject<LogNode>(bytes);
//#else
//                var node = EntityManager.ResolversMap.Deserialize<LogNode>(bytes);
//#endif
               
//                nodes.Add(node);
//            }

//            data.Nodes = nodes;
//        }
//    }
//}