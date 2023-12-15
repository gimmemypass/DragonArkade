using HECSFramework.Core;

namespace Components
{
    public partial class TransformComponent : BaseComponent
    {
        [HideInInspectorCrossPlatform]
        [Field(0)]
        public Vector3Serialize PositionSave;

        [HideInInspectorCrossPlatform]
        [Field(1)]
        public Vector3Serialize RotationSave;

        public bool IsDirty { get; set; }
    }
}