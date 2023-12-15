using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
    public struct RemoveItemToCharacterCommand : ICommand
    {
        public Entity Item;
    }
}