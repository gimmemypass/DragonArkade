using System;
using System.Collections.Generic;
using HECSFramework.Core;

namespace Components
{
    [Serializable]
    [Documentation(Doc.Strategy, Doc.HECS, Doc.Counters, "here we cache counters when we needed from strategies")]
    public sealed class CacheCounterValuesComponent : BaseComponent, IDisposable
    {
        public Dictionary<int, float> Values = new Dictionary<int, float>();

        public void Dispose()
        {
            Values.Clear();
        }
    }
}