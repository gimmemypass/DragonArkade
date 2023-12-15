namespace Components
{
    public partial class ModifiableFloatCounter
    {
        public void Recalc()
        {
            modifiersContainer.SetDirty();
            UpdatValueWithModifiers(currentValue, modifiersContainer.GetCalculatedValue());
        }

        public void RecalcAndSetMax()
        {
            modifiersContainer.SetDirty();
            SetValue(modifiersContainer.GetCalculatedValue()); 
        }
    }
}