namespace Components
{
    public abstract partial class ModifiableFloatCounterComponent
    {
        public void Recalc() => modifiableFloatCounter.Recalc();
        public void RecalcAndSetMax() => modifiableFloatCounter.RecalcAndSetMax();
    }
}