using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.AI, Doc.Strategy, "Forcing ai to start, remove stop and set is need decision")]
    public struct ForceStartAICommand : ICommand
    {
    }

    [Documentation(Doc.AI, Doc.Strategy, "Forcing ai to stop")]
    public struct ForceStopAICommand : ICommand
    {
    }
}