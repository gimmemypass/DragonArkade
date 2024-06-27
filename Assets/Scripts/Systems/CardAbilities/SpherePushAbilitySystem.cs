using System;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using Random = Unity.Mathematics.Random;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Sphere, Doc.Abilities, "ability to push")]
    public sealed class SpherePushAbilitySystem : BaseCardAbilitySystem
    {
        private const float AttackAnimationRange = 3;
        private EntitiesFilter spheresFilter;

        public override void InitSystem()
        {
            spheresFilter = EntityManager.Default.GetFilter<SphereComponent>();
        }

        public override UniTask ExecuteCard(Entity owner = null, Entity target = null, bool enable = true)
        {
            //animation
            target!.Command(new TriggerAnimationCommand(){Index = AnimParametersMap.IsAttacking});
            foreach (var sphere in spheresFilter)
            {
                var direction = target.GetComponent<PushDirectionComponent>().Direction;
                var sphereComponent = sphere.GetComponent<SphereComponent>();
                sphereComponent.Dir = direction;
            }
            return UniTask.CompletedTask;
        }
    }
}