using System;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class RendererProviderComponent : BaseProviderComponent<Renderer>
    {
    }
}