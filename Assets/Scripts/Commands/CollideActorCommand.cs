using HECSFramework.Core;
using HECSFramework.Unity;

namespace Commands
{
    [Documentation(Doc.GameLogic, "")]
    public struct CollideActorCommand : ICommand
    {
        public Actor Actor;
    }
}