using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, Doc.Arena, "search for nearest drop item")]
    public sealed class SearchNearestDropItemSystem : BaseSystem, IUpdatable
    {
        [Required] public NearestDropItemsComponent NearestDropItemsComponent;
        [Required] public UnityTransformComponent UnityTransformComponent;
        
        private EntitiesFilter dropItemsFilter;

        public override void InitSystem()
        {
            dropItemsFilter = EntityManager.Default.GetFilter<DropItemComponent>();
        }

        public void UpdateLocal()
        {
            Entity nearest = null;
            float minDist = float.MaxValue;
            foreach (var dropItem in dropItemsFilter)
            {
                var position = UnityTransformComponent.Transform.position;
                var itemPos = dropItem.GetComponent<UnityTransformComponent>().Transform.position;
                var dist = (itemPos - position).sqrMagnitude;
                if (minDist > dist)
                {
                    minDist = dist;
                    nearest = dropItem;
                }
            }
            NearestDropItemsComponent.DropItem = nearest;
        }
    }
}