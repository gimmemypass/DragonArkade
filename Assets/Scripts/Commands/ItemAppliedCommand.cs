using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
    public struct ItemAppliedCommand : IGlobalCommand
    {
        public Entity Owner;
        public Entity Item;
    }
}