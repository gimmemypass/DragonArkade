using System;
using System.Collections.Generic;
using Commands;
using Components;
using Components.MonoBehaviourComponents;
using HECSFramework.Core;
using HECSFramework.Core.Helpers;
using HECSFramework.Unity;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class RotateCharacterItemsSystem : BaseAbilitySystem,
        IUpdatable,
        IReactCommand<AddItemToCharacterCommand>,
        IReactCommand<RemoveItemToCharacterCommand>
    {
        [Required] public CharacterItemsComponent CharacterItemsComponent;

        private CharactersItemsHolderMonoComponent monoComponent;

        private Dictionary<int, float> entityIdToAngle = new();

        public override void InitSystem()
        {
        }

        public void UpdateLocal()
        {
            if (!Owner.ContainsMask<VisualInActionTagComponent>())
                return;
            monoComponent.transform.rotation *= Quaternion.Euler(0, CharacterItemsComponent.RotationSpeed * Time.deltaTime, 0); 
            UpdatePositionsAround();
        }

        public void CommandReact(AddItemToCharacterCommand command)
        {
            CharacterItemsComponent.Items.Add(command.Item.Index, command.Item);
            
            var itemTransform = command.Item.GetComponent<UnityTransformComponent>();
            itemTransform.Transform.SetParent(monoComponent.transform);
            itemTransform.Transform.position = monoComponent.transform.position + Vector3.forward * CharacterItemsComponent.Radius;
            UpdateAngles();
            UpdatePositionsAround();
        }

        private void UpdateAngles()
        {
            var angleStep = 360f / CharacterItemsComponent.Items.Count;
            int i = 0;
            foreach (var item in CharacterItemsComponent.Items)
            {
                if (!item.Value.IsAlive)
                    return;
                var angle = angleStep * i + monoComponent.transform.rotation.eulerAngles.y;
                entityIdToAngle.AddOrReplace(item.Value.Index, angle);
                i++;
            }
        }

        private void UpdatePositionsAround()
        {
            foreach (var item in CharacterItemsComponent.Items)
            {
                if (!item.Value.IsAlive)
                    return;
                var itemTransform = item.Value.GetComponent<UnityTransformComponent>();
                itemTransform.Transform.position = monoComponent.transform.position + Vector3.forward * CharacterItemsComponent.Radius;
                var angle = entityIdToAngle[item.Value.Index] + monoComponent.transform.rotation.eulerAngles.y;
                itemTransform.Transform.RotateAround(monoComponent.transform.position, Vector3.up, angle);
                itemTransform.Transform.rotation = Quaternion.Euler(0,angle, 0);
            }
        }
        
        public void CommandReact(RemoveItemToCharacterCommand command)
        {
            command.Item.GetComponent<UnityTransformComponent>().Transform.SetParent(null);
            CharacterItemsComponent.Items.Remove(command.Item.Index);
            entityIdToAngle.Remove(command.Item.Index);
        }

        public override void Execute(Entity owner = null, Entity target = null, bool enable = true)
        {
            owner.AsActor().TryGetComponent(out monoComponent, true);
        }
    }
}