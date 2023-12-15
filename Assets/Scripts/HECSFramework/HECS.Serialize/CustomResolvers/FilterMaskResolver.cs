using System;
using HECSFramework.Serialize;
using MessagePack;

namespace HECSFramework.Core
{
    [Serializable]
    [MessagePackObject()]
    public struct FilterMaskResolver : IResolver<FilterMaskResolver, Filter>
    {
        [Key(0)]
        public int Length;

        [Key(1)]
        public HECSMaskResolver Mask1;
        [Key(2)]
        public HECSMaskResolver Mask2;
        [Key(3)]
        public HECSMaskResolver Mask3;
        [Key(4)]
        public HECSMaskResolver Mask4;
        [Key(5)]
        public HECSMaskResolver Mask5;
        [Key(6)]
        public HECSMaskResolver Mask6;

        public FilterMaskResolver In(ref Filter data)
        {
            Length = data.Lenght;
            Mask1 = new HECSMaskResolver().In(ref data.Mask01);
            Mask2 = new HECSMaskResolver().In(ref data.Mask02);
            Mask3 = new HECSMaskResolver().In(ref data.Mask03);
            Mask4 = new HECSMaskResolver().In(ref data.Mask04);
            Mask5 = new HECSMaskResolver().In(ref data.Mask05);
            Mask6 = new HECSMaskResolver().In(ref data.Mask06);
            return this;
        }

        public void Out(ref Filter data)
        {
            data.Lenght = Length;
            Mask1.Out(ref data.Mask01);
            Mask2.Out(ref data.Mask01);
            Mask3.Out(ref data.Mask01);
            Mask4.Out(ref data.Mask01);
            Mask5.Out(ref data.Mask01);
            Mask6.Out(ref data.Mask01);
        }
    }
}