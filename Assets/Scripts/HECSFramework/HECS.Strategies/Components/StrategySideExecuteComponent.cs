using System;
using HECSFramework.Core;

namespace Components
{
    [Serializable][Documentation(Doc.Strategy, Doc.HECS,  "this component we need when we start side strategy for some entity")]
    public sealed class StrategySideExecuteComponent : BaseComponent, IPoolableComponent, IDisposable
    {
        public Entity StrategyOwner;
        public Entity StrategyTarget;
        public Entity StrategyInitiation;

        public void Dispose()
        {
            StrategyOwner = null;
            StrategyTarget = null;
            StrategyInitiation = null;
        }
    }
}