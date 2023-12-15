using System.Collections.Generic;
using HECSFramework.Core;

namespace HECSFramework.Serialize
{
    [Documentation(Doc.HECS, Doc.Animation, "this component holds animation state and links identifiers id to AnimatorHashID")]
    public partial class AnimatorState : ISaveToResolver<AnimatorStateResolver>, ILoadFromResolver<AnimatorStateResolver>
    {
        private Dictionary<int, BoolParameter> boolParameters = new Dictionary<int, BoolParameter>();
        private Dictionary<int, FloatParameter> floatParameters = new Dictionary<int, FloatParameter>();
        private Dictionary<int, IntParameter> intParameters = new Dictionary<int, IntParameter>();

        public int AnimatorID { get; protected set; }

        #region Constructor
        public AnimatorState() 
        {
        }

        public AnimatorState(int animatorID, Dictionary<int, BoolParameter> boolParameters, Dictionary<int, FloatParameter> floatParameters, Dictionary<int, IntParameter> intParameters)
        {
            AnimatorID = animatorID;
            this.boolParameters = boolParameters;
            this.floatParameters = floatParameters;
            this.intParameters = intParameters;
        }
        #endregion

        public void SetBool(int id, bool value, bool forceSet = false)
        {
            if (boolParameters.TryGetValue(id, out var parameter))
                parameter.Set(value, forceSet);
            else
                HECSDebug.LogWarning("we dont have parameter " + id);
        }

        public void SetFloat(int id, float value, bool forceSet = false)
        {
            if (floatParameters.TryGetValue(id, out var parameter))
                parameter.Set(value, forceSet   );
            else
                HECSDebug.LogWarning("we dont have parameter " + id);
        }

        public void SetInt(int id, int value, bool forceSet = false)
        {
            if (intParameters.TryGetValue(id, out var parameter))
                parameter.Set(value, forceSet);
            else
                HECSDebug.LogWarning("we dont have parameter " + id);
        }

        public bool TryGetBool(int id ,out BoolParameter value)
        {
            return boolParameters.TryGetValue(id, out value);
        }

        public bool TryGetFloat(int id, out FloatParameter value)
        {
            return floatParameters.TryGetValue(id, out value);
        }

        #region SaveLoad
        public void Load(ref AnimatorStateResolver resolver)
        {
            foreach (var bp in resolver.BoolStates)
            {
                if (boolParameters.ContainsKey(bp.Key))
                    boolParameters[bp.Key].Set(bp.Value.Value);
                else
                    boolParameters.Add(bp.Key, new BoolParameter(bp.Key, bp.Value.Value));
            }

            foreach (var bp in resolver.IntStates)
            {
                if (intParameters.ContainsKey(bp.Key))
                    intParameters[bp.Key].Set(bp.Value.Value);
                else
                    intParameters.Add(bp.Key, new IntParameter(bp.Key, bp.Value.Value));
            }

            foreach (var bp in resolver.FloatStates)
            {
                if (floatParameters.ContainsKey(bp.Key))
                    floatParameters[bp.Key].Set(bp.Value.Value);
                else
                    floatParameters.Add(bp.Key, new FloatParameter(bp.Key, bp.Value.Value));
            }

            AnimatorID = resolver.AnimatorID;
        }

        public void Save(ref AnimatorStateResolver resolver)
        {
            foreach (var bp in boolParameters)
                resolver.BoolStates.Add(bp.Key, new BoolParameterResolver { Value = bp.Value.Value });

            foreach (var bp in intParameters)
                resolver.IntStates.Add(bp.Key, new IntParameterResolver { Value = bp.Value.Value });

            foreach (var bp in floatParameters)
                resolver.FloatStates.Add(bp.Key, new FloatParameterResolver { Value = bp.Value.Value });
        }
        #endregion
    }
}