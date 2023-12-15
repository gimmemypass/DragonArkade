using System;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using HECSFramework.Core;
using Helpers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Sphere, Doc.Abilities, "")]
    public sealed class ExplodeSpheresAbilitySystem : BaseCardAbilitySystem
    {
        [Required] public ExplodeSpheresAbilityComponent Component;
        [Required] public DamageComponent DamageComponent;
        private EntitiesFilter spheresFilter;
        private RaycastHit[] raycastHits;

        public override void InitSystem()
        {
            spheresFilter = EntityManager.Default.GetFilter<SphereComponent>();
            raycastHits = new RaycastHit[16];
        }

        public override UniTask ExecuteCard(Entity owner = null, Entity target = null, bool enable = true)
        {
            foreach (var sphere in spheresFilter)
            {
                var position = sphere.GetComponent<UnityTransformComponent>().Transform.position;
                var particleSystem = Object.Instantiate(Component.ParticleSystem, position, Quaternion.identity);
                particleSystem.Play();
                var count = Physics.SphereCastNonAlloc(position, Component.Radius, Vector3.up, raycastHits, Component.Radius);
                for (int i = 0; i < count; i++)
                {
                    if (raycastHits[i].collider.TryGetActorFromCollision(out var actor) && actor != null)
                    {
                        actor.Command(new DamageCommand<float> {DamageDealer = target, DamageValue = DamageComponent.Value, DmgType = 0});
                    }
                }
            }
            return UniTask.CompletedTask;
        }
    }
}