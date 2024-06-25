using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.GameLogic, "Command to add item")]
    public struct AddItemToCharacterCommand : ICommand
    {
        public Entity Item;
    }
}