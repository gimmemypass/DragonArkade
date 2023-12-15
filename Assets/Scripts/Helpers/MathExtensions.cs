namespace Helpers
{
    public static class MathExtensions
    {
        public static float GetClampedValueFromZeroToOne(float originalValue, float minOriginalRange, float maxOriginalRange,
            float minNewRange, float maxNewRange)
        {
            var newValue = minNewRange + (((maxNewRange - minNewRange) * (originalValue - minOriginalRange)) /
                                          (maxOriginalRange - minOriginalRange));
            return newValue;
        }
    }
}