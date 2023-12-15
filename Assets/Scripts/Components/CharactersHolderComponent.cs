using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.GameLogic, "")]
    public sealed class CharactersHolderComponent : BaseComponent, IWorldSingleComponent
    {
        public ActorContainer SphereContainer;
    }
}