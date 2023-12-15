using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class ItemThrowingSystem : BaseSystem, IReactCommand<TryApplyItemCommand>
    {

        public override void InitSystem()
        {
            
        }

        public void CommandReact(TryApplyItemCommand command)
        {
            Owner.GetComponent<DirectionComponent>().Direction = Owner.GetComponent<UnityTransformComponent>().Transform.forward;
            Owner.GetComponent<BelongingComponent>().Entity.Command(new ItemAppliedCommand(){Item = Owner});
        }
    }
}