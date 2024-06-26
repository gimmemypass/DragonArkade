using System;
using System.Collections.Generic;
using HECSFramework.Core;
using UnityEngine;

namespace Strategies
{


    [Serializable]
    public struct ConnectionContext : IEquatable<ConnectionContext>
    {
        public string In;
        public string Out;

        public override bool Equals(object obj)
        {
            return obj is ConnectionContext context &&
                   In == context.In &&
                   Out == context.Out;
        }

        public bool Equals(ConnectionContext other)
        {
            return In == other.In && Out == other.Out;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(In, Out);
        }
    }


    public abstract class InterDecision : LogNode
    {
        [Connection(ConnectionPointType.In, "Input")] [IgnoreDraw] public BaseDecisionNode parent;
        [Connection(ConnectionPointType.Out, "Next")] [IgnoreDraw] public BaseDecisionNode Next;
    }

    public abstract class SetNode : LogNode
    {
        [Connection(ConnectionPointType.Out, "Set")][IgnoreDraw] public BaseDecisionNode Set;
    }

    public abstract class DilemmaDecision : LogNode
    {
        [Connection(ConnectionPointType.In, "Input")] public BaseDecisionNode Input;
        [Connection(ConnectionPointType.Out, "Positive")] public BaseDecisionNode Positive;
        [Connection(ConnectionPointType.Out, "Negative")] public BaseDecisionNode Negative;
    }

    public interface IDecisionNode
    {
        void Execute(Entity entity);
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ConnectionAttribute : Attribute
    {
        public ConnectionPointType ConnectionPointType;
        public string NameOfField = "Field";

        public ConnectionAttribute(ConnectionPointType connectionPointType, string nameOfField)
        {
            ConnectionPointType = connectionPointType;
            NameOfField = nameOfField;
        }
    }

    public enum ConnectionPointType { In, Out }
}