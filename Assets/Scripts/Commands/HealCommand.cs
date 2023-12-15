using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.Character, Doc.Stats, "HealCommand")]
    public struct HealCommand : ICommand
    {
        public float Amount;
    }
}