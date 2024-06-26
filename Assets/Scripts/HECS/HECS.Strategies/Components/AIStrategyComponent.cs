using HECSFramework.Core;
using Strategies;
using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NPC, Doc.AI, Doc.Strategy, "Компонент который содержит в себе стратегию поведения моба и дефолтный стейт")]
    public class AIStrategyComponent : BaseComponent
    {
        [SerializeField] private Strategy baseStrategy;
        [SerializeField] private bool manualStart;
        
        public bool ManualStart { get => manualStart; }
        public Strategy Strategy => baseStrategy;
    }
}