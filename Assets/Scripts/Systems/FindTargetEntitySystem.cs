using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class FindTargetEntitySystem : BaseSystem, ICustomUpdatable
    {
        [Required] public FactionComponent FactionComponent;
        [Required] public TargetEntityComponent TargetEntityComponent;
        [Required] public UnityTransformComponent UnityTransformComponent;
        private EntitiesFilter charactersFilter;

        public override void InitSystem()
        {
            charactersFilter = EntityManager.Default.GetFilter<FactionComponent>();
        }

        public YieldInstruction Interval { get; } = new WaitForSeconds(1f);

        public void UpdateCustom()
        {
            var mainCharPos = UnityTransformComponent.Transform.position;
            Entity nearest = null;
            float nearestDist = float.MaxValue;
            foreach (var character in charactersFilter)
            {
                if(character.GetComponent<FactionComponent>().FactionIdentifier.Id == FactionComponent.FactionIdentifier.Id) 
                    continue;
                var dist = (character.GetComponent<UnityTransformComponent>().Transform.position - mainCharPos).sqrMagnitude;
                if (dist < nearestDist)
                {
                    nearest = character;
                    nearestDist = dist;
                }
            } 
            TargetEntityComponent.Target = nearest;
        }
    }
}