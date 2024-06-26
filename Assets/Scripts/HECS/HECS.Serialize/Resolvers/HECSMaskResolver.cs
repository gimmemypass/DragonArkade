using System;
using HECSFramework.Serialize;
using MessagePack;

namespace HECSFramework.Core
{
    [Serializable]
    [MessagePackObject()]
    public struct HECSMaskResolver : IResolver<HECSMaskResolver, HECSMask>, IResolver<HECSMaskResolver, int>
    {
        [Key(0)]
        public int TypeHashCode;
        
        public HECSMaskResolver In(ref HECSMask data)
        {
            TypeHashCode = data.TypeHashCode;
            return this;
        }

        public HECSMaskResolver In(ref int data)
        {
            TypeHashCode = data;
            return this;
        }

        public void Out(ref HECSMask data)
        {
            if (TypesMap.GetComponentInfo(TypeHashCode, out var mask))
            {
                data.TypeHashCode = mask.ComponentsMask.TypeHashCode;
                data.Index = mask.ComponentsMask.Index;
            }
            else
            {
                HECSDebug.LogError("hecs mask resolver contains wrong mask " + mask.ComponentsMask.TypeHashCode);
            }
        }

        public void Out(ref int data)
        {
            data = TypeHashCode;
        }
    }
}