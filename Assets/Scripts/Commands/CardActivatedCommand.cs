using HECSFramework.Core;

namespace Commands
{
    //only activating before handling ability
    [Documentation(Doc.NONE, "")]
    public struct CardActivatedCommand : IGlobalCommand
    {
        public Entity Entity;
    }
    
    [Documentation(Doc.NONE, "")]
    public struct CardAbilityHandledCommand : IGlobalCommand
    {
    }
}