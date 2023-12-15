using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class FindTargetEntitySystem : BaseSystem, IUpdatable
    {
        [Required] public FactionComponent FactionComponent;
        [Required] public TargetEntityComponent TargetEntityComponent;
        private EntitiesFilter charactersFilter;

        public override void InitSystem()
        {
            charactersFilter = EntityManager.Default.GetFilter<FactionComponent>();
        }

        public void UpdateLocal()
        {
            if (TargetEntityComponent.Target == null)
            {
                foreach (var character in charactersFilter)
                {
                    if(character.GetComponent<FactionComponent>().FactionIdentifier.Id == FactionComponent.FactionIdentifier.Id) 
                        continue;
                    TargetEntityComponent.Target = character;
                    return;
                } 
            } 
        }
    }
}