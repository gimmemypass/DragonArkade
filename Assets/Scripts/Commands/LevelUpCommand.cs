using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
    public struct LevelUpCommand : IGlobalCommand
    {
        public int CurrentLevel;
    }
}