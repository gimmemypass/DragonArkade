using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
    public struct ItemAppliedCommand : ICommand
    {
        public Entity Item;
    }
}