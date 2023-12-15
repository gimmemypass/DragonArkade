using HECSFramework.Core;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Strategies
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public abstract class BaseDecisionNode : ScriptableObject, IDecisionNode
    {
        public abstract string TitleOfNode { get; }
        [IgnoreDraw] public Vector2 coords;

        [IgnoreDraw]
        [HideInInspectorCrossPlatform]
        public List<ConnectionContext> ConnectionContexts = new List<ConnectionContext>();

        public abstract void Execute(Entity entity);
    }
}