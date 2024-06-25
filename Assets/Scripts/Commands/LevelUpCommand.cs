using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.GameLogic, "level up command")]
    public struct LevelUpCommand : IGlobalCommand
    {
        public int CurrentLevel;
    }
}