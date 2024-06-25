using HECSFramework.Core;

namespace Commands
{
    //only activating before handling ability
    [Documentation(Doc.Cards, "Command to activate card by entity")]
    public struct CardActivatedCommand : IGlobalCommand
    {
        public Entity Entity;
    }
    
    [Documentation(Doc.Cards, "")]
    public struct CardAbilityHandledCommand : IGlobalCommand
    {
    }
}