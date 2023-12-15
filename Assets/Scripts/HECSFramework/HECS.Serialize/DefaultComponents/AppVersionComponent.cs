using HECSFramework.Core;

namespace Components
{
    public partial class AppVersionComponent 
    {
        [Field(0)]
        public int Version { get => version; set => version = value; }
    }
}