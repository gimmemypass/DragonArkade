using System;
using HECSFramework.Core;

namespace Components
{
    [Serializable][Documentation(Doc.Character, Doc.UI, Doc.Enemy, "This components marks enemy whom need hp bar")]
    public sealed class ShowHpBarTagComponent : BaseComponent
    {
        public Entity HpBar;
        public bool IsUpdated;
    }
}