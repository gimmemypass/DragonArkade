using System;
using HECSFramework.Core;
using Strategies;

namespace HECSFramework.Unity.Strategies
{
    [Serializable]
    [Documentation(Doc.Strategy, "this node remove component")]
    public class RemoveComponentNode : InterDecision, IInitable
    {
        public override string TitleOfNode { get; } = "Remove Component";

        [Connection(ConnectionPointType.In, "<Entity> Optional Target")]
        public GenericNode<Entity> OptionalTarget;

        [Field(0)]
        [ComponentMaskDropDown]
        public int ComponentMask;

        protected override void Run(Entity entity)
        {
            Entity target = null;

            if (OptionalTarget != null)
                target = OptionalTarget.Value(entity);
            else
                target = entity;

            target.RemoveComponent(ComponentMask);
            Next.Execute(entity);
        }

        //we can have situation when mask have right typecode but wrong index, and we should validate hecsmask anyway,
        //mby this component not available anymore
        public void Init()
        {
            if (!TypesMap.ContainsComponent(ComponentMask))
                HECSDebug.LogError("is have component node contains wrong mask " + ComponentMask);
        }
    }
}