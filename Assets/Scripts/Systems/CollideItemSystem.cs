using System;
using Commands;
using Components;
using HECSFramework.Core;
using Helpers;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class CollideItemSystem : BaseSystem, IReactCommand<TriggerEnterCommand>
    {
        [Required] public DamageComponent DamageComponent;
        [Required] public BelongingComponent BelongingComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(TriggerEnterCommand command)
        {
            if (!command.Collider.TryGetActorFromCollision(out var actor)
                || actor == null
                || !actor.Entity.ContainsMask<HealthComponent>()
                || actor.Entity.ID == BelongingComponent.Entity.ID
                || (actor.Entity.TryGetComponent(out BelongingComponent belongingComponent) && belongingComponent.Entity.ID  == BelongingComponent.Entity.ID))
                return;
            
            actor.Command(new DamageCommand<float>()
            {
                DamageDealer = BelongingComponent.Entity,
                DamageValue = DamageComponent.Value
            });
            EntityManager.Command(new DestroyEntityWorldCommand(){Entity = Owner});
        }

    }
}