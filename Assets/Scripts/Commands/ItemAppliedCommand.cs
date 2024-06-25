using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.GameLogic, "when item applied")]
    public struct ItemAppliedCommand : IGlobalCommand
    {
        public Entity Owner;
        public Entity Item;
    }
}