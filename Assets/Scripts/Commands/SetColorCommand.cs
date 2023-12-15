using HECSFramework.Core;
using UnityEngine;

namespace Commands
{
    [Documentation(Doc.GameLogic, "local command to change color")]
    public struct SetColorCommand : ICommand
    {
        public Color Color;
    }
}