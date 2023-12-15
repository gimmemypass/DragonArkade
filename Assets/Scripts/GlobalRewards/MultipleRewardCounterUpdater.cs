using System;
using UnityEngine;

namespace Components
{
    public class MultipleRewardCounterUpdater
    {
        private readonly float countPerElement;
        private readonly Action<int> updateVisualCallback;
        private float addedCount;

        public MultipleRewardCounterUpdater(float countPerElement, Action<int> updateVisualCallback)
        {
            this.updateVisualCallback = updateVisualCallback;
            this.countPerElement = countPerElement;
        }
        
        public void OnSingleItemComplete()
        {
            var diff = countPerElement - (int)countPerElement;
            addedCount += diff;
            updateVisualCallback?.Invoke((int)countPerElement);
        }

        public void OnAllItemsCompleted()
        {
            var countToAdd = Mathf.RoundToInt(addedCount);
            updateVisualCallback?.Invoke(countToAdd);
        }
    }
}