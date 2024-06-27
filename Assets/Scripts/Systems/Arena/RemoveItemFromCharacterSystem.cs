using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, Doc.Arena, "handle removing item from character")]
    public sealed class RemoveItemFromCharacterSystem : BaseSystem
    {
        public override void InitSystem()
        {
        }

        public override void BeforeDispose()
        {
            base.BeforeDispose();
            Owner.GetComponent<BelongingComponent>()?.Entity?.Command(new RemoveItemToCharacterCommand(){Item = Owner});
        }
    }
}