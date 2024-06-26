using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Strategies;

namespace HECSFramework.Core
{
    public static class StrategyResolverHelper
    {
        public static IEnumerable<FieldInfo> GetConnectionFields(Type nodeType)
        {
            return nodeType
                .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Where(f => Attribute.IsDefined(f, typeof(ConnectionAttribute)))
                .Where(f => typeof(BaseDecisionNode).IsAssignableFrom(f.FieldType));
        }

        public static NodeContext SerializeNode(Type nodeType, BaseDecisionNode node, byte[] nodeData,
            Dictionary<BaseDecisionNode, int> nodeIndices)
        {
            var connectionFields = GetConnectionFields(nodeType);

            var connections = new List<ConnectionInfo>();
            foreach (var nodeField in connectionFields)
            {
                var connected = (BaseDecisionNode)nodeField.GetValue(node);
                if (connected != null)
                    connections.Add(new ConnectionInfo
                    {
                        Name = nodeField.Name,
                        NodeIndex = nodeIndices[connected]
                    });
            }

            NodeContext nodeContext = new NodeContext
            {
                Index = nodeIndices[node],
                NodeData = nodeData,
                Connections = connections
            };
            return nodeContext;
        }
    }
}