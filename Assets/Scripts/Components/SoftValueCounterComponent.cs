using System;
using HECSFramework.Core;
using HECSFramework.Serialize;

namespace Components
{
    [Serializable]
    [JSONHECSSerialize]
    [Documentation(Doc.NONE, "")]
    public sealed partial class SoftValueCounterComponent : SimpleIntCounterBaseComponent, ISavebleComponent
    {
        [Field(0)]
        private int value;
        public override int Id => CounterIdentifierContainerMap.SoftValue;

        public override int Value
        {
            get => value;
            protected set => this.value = value;
        }
    }
}