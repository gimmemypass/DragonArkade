using System;
using HECSFramework.Core;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class TimeScaleComponent : BaseComponent, IWorldSingleComponent
    {
        public float PrevScale;
        public float Scale;
    }
}