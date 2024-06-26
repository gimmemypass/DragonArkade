using System;
using HECSFramework.Core;

namespace Strategies
{
    [Serializable]
    [Documentation(Doc.Strategy, "this node checks => entity have this component or not")]
    [HECSResolver]
    public class IsHaveComponentNode : DilemmaDecision, IInitable
    {
        public override string TitleOfNode { get; } = "Is Have Component";

        [Connection(ConnectionPointType.In, "<Entity> Optional Target")]
        public GenericNode<Entity> OptionalTarget;

        [ComponentMaskDropDown]
        [Field(0)]
        public int ComponentMask;

        protected override void Run(Entity entity)
        {
            Entity target = null;

            if (OptionalTarget != null)
                target = OptionalTarget.Value(entity);
            else
                target = entity;

            if (target.ContainsMask(ComponentMask))
                Positive.Execute(entity);
            else
                Negative.Execute(entity);
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