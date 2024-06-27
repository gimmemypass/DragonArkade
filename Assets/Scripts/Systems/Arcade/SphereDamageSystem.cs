using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "damage when collided")]
    public sealed class SphereDamageSystem : BaseSystem, IReactCommand<CollideActorCommand>
    {
        [Required] public DamageComponent DamageComponent;
        public override void InitSystem()
        {
        }

        public void CommandReact(CollideActorCommand command)
        {
            command.Actor.Command(new DamageCommand<float>()
            {
                DamageValue = DamageComponent.Value,
                DamageDealer = Owner
            }); 
        }
    }
}