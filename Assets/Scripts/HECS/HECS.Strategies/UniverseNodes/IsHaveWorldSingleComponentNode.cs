using System;
using HECSFramework.Core;

namespace Strategies
{
    [Serializable]
    [HECSResolver]
    [Documentation(Doc.Strategy, "this node checks => world have this component or not")]
    public class IsHaveWorldSingleComponentNode : DilemmaDecision, IInitable
    {
        public override string TitleOfNode { get; } = "Is Have World Single Component";

        [Field(0)]
        [ComponentMaskDropDown]
        public int ComponentMask;

        protected override void Run(Entity entity)
        {
            if (entity.World.IsHaveSingleComponent(ComponentMask))
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