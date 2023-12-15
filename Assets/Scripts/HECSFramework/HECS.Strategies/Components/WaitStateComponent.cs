using HECSFramework.Core;
using System;

namespace Components
{
    [Serializable]
    [Documentation(Doc.AI, "Компонент который нужен для стейта Wait, в котором мы указываем сколько мы планируем пробыть в этом состоянии")]
    public class WaitStateComponent : BaseComponent
    {
        public float CurrentWaitTimer = 1;
    }
}