using System;
using Components;
using HECSFramework.Core;
using Strategies;

namespace BluePrints.Strategies
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class SetRigidbodyIsKinematicNode : InterDecision
    {
        [ExposeField]
        public bool IsKinematic;
        public override string TitleOfNode { get; } = "Set rb iskinematic";

        protected override void Run(Entity entity)
        {
            entity.GetComponent<RigidbodyProviderComponent>().Get.isKinematic = IsKinematic;
            Next.Execute(entity);
        }
    }
}