using System;
using HECSFramework.Core;
using HECSFramework.Serialize;

namespace Components
{
    [Serializable][Documentation(Doc.Animation, Doc.HECS, "This component holds serialized state of animator, you need to set the animator parameters through this component")]
    public sealed partial  class AnimatorStateComponent : BaseComponent
    {
        [Field(0, typeof(AnimatorStateResolver))]
        public AnimatorState State;
    }
}
