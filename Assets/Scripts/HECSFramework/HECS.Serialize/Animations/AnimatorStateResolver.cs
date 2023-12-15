using System;
using System.Collections.Generic;
using Components;
using HECSFramework.Core;
using MessagePack;

namespace HECSFramework.Serialize
{
    [MessagePackObject]
    public struct AnimatorStateResolver : IResolver<AnimatorState>, IResolver<AnimatorStateResolver, AnimatorState>
    {
        [Key(0)]
        public Dictionary<int, BoolParameterResolver> BoolStates;
        [Key(1)]
        public Dictionary<int, IntParameterResolver> IntStates;
        [Key(2)]
        public Dictionary<int, FloatParameterResolver> FloatStates;

        [Key(3)]
        public int AnimatorID;

        public AnimatorStateResolver In(ref AnimatorState data)
        {
            BoolStates = new Dictionary<int, BoolParameterResolver>();
            IntStates = new Dictionary<int, IntParameterResolver>();
            FloatStates = new Dictionary<int, FloatParameterResolver>();
            AnimatorID = data.AnimatorID;
            data.Save(ref this);
            return this;
        }

        public void Out(ref AnimatorState data)
        {
            data.Load(ref this);
        } 

        public void Out(ref Entity entity)
        {
            entity.GetComponent<AnimatorStateComponent>().State.Load(ref this);
        }
    }
}
