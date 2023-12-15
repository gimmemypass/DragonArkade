using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    public partial class TransformComponent : IAfterSerializationComponent, IBeforeSerializationComponent
    {
        public void AfterSync()
        {
            Position = PositionSave.AsVector;
            Rotation = Quaternion.Euler(RotationSave.AsVector);
            Interpolate();
        }
        partial void Interpolate();

        public void BeforeSync()
        {
            PositionSave = new Vector3Serialize(Position);
            RotationSave = new Vector3Serialize(Rotation.eulerAngles);
        }
    }
}