using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.NONE, "")]
    public struct AddItemToCharacterCommand : ICommand
    {
        public Entity Item;
    }
}