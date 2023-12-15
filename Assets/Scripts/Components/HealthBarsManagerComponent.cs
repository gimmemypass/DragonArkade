using System;
using HECSFramework.Core;
using UnityEngine.Rendering;

namespace Components
{
    [Serializable]
    [Documentation(Doc.UI, Doc.Global, "Contains health bars")]
    public sealed class HealthBarsManagerComponent : BaseComponent, IWorldSingleComponent
    {
        public ObservableList<ShowHpBarTagComponent> ShowHpBarTags = new();
    }
}