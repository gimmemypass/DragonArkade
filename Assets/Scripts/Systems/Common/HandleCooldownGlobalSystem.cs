using System;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "handle cooldown global system")]
    public sealed class HandleCooldownGlobalSystem : BaseSystem, IUpdatable
    {
        private EntitiesFilter filter;

        public override void InitSystem()
        {
            filter = EntityManager.Default.GetFilter<CooldownComponent>();
        }

        public void UpdateLocal()
        {
            foreach (var ent in filter)
            {
                var cooldownComponent = ent.GetComponent<CooldownComponent>();
                if (cooldownComponent.Value > 0)
                {
                    cooldownComponent.SetValue(cooldownComponent.Value - Time.deltaTime);
                }
            }
        }
    }
}