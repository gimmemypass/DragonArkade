using System;
using HECSFramework.Core;
using VFX;

namespace Commands
{
    [Documentation(Doc.Visual, Doc.FX, "command for collection of fx like money to ui")]
    public struct MultipleItemsFxCommand : IGlobalCommand
    {
        public EffectData EffectData;
        public Action OnSingleItemComplete;
        public Action OnAllItemsComplete;
    }
}