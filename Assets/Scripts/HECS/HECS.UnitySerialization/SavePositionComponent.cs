using HECSFramework.Core;
using System;

namespace Components
{
    [Serializable]
    [Documentation("Actor", "Character", "���� ��������� �������� � ���� ���������� ������ � ������� � �������� ������, ��� ������� ����� �������� ��� ������ �� ������������� ����������")]
    public class SavePositionComponent : BaseComponent, IBeforeSerializationComponent
    {
        [Field(0)]
        public Vector3Serialize Position;
        
        [Field(1)]
        public Vector3Serialize Rotation;

        public void BeforeSync()
        {
            if (Owner.TryGetComponent(out TransformComponent transformComponent))
            {
                transformComponent.BeforeSync();
                Position = transformComponent.PositionSave;
                Rotation = transformComponent.RotationSave;
            }
        }
    }
}