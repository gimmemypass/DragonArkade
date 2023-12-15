using System.Collections.Generic;
using HECSFramework.Core;

namespace Components
{
    public sealed partial class CountersHolderComponent : BaseComponent, IBeforeSerializationComponent, IAfterSerializationComponent
    {
        public List<ICounter> countersToSave = new List<ICounter>(16);

        public void AfterSync()
        {
            foreach (var counter in countersToSave)
            {
                SetOrAddCounter(counter);
            }
        }

        public void BeforeSync()
        {
            countersToSave.Clear();

            foreach (var counter in counters)
            {
                //we dont need save and send components, they should sync separatly
                if (counter.Value is IComponent)
                    continue;

                countersToSave.Add(counter.Value);
            }
        }
    }
}
