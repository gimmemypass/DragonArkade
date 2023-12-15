using HECSFramework.Core;

namespace Commands
{
    [Documentation(Doc.Save, "Command for saving player entity")]
    public struct SaveCommand : IGlobalCommand
    {
    }
    
    [Documentation(Doc.Save, "Command before saving player entity")]
    public struct BeforeSaveCommand : IGlobalCommand
    {
    }
}