using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HECSFramework.Core;
using HECSFramework.Serialize;
using static HECSFramework.Serialize.CopyExtention;

namespace Systems
{
    [Documentation(Doc.Serialization, "this part of pooling system have responsibility for pooling of thread safe  component copy processor")]
    public sealed partial class PoolingSystem : IUpdatable, IAfterEntityInit
    {
        private struct ReturnToPoolContainer
        {
            public int CurrentTick;
            public IThreadSafeComponentCopyProcessor threadSafeComponentCopy;

            public override bool Equals(object obj)
            {
                return obj is ReturnToPoolContainer container &&
                       threadSafeComponentCopy.InstanceGuid == container.threadSafeComponentCopy.InstanceGuid;
            }

            public override int GetHashCode()
            {
                return threadSafeComponentCopy.InstanceGuid.GetHashCode();
            }
        }


        private int ticksToPass = 10;

        private ConcurrentDictionary<int, Stack<IThreadSafeComponentCopyProcessor>> poolOfProcessors = new ConcurrentDictionary<int, Stack<IThreadSafeComponentCopyProcessor>>();
        private HECSList<IThreadSafeComponentCopyProcessor> threadSafeComponentCopyProcessors = new HECSList<IThreadSafeComponentCopyProcessor>(16);
        private HECSList<(int time, IThreadSafeComponentCopyProcessor safeProcessor)> returnToPool = new HECSList<(int time, IThreadSafeComponentCopyProcessor safeProcessor)>(16);
        private Remover<IThreadSafeComponentCopyProcessor> removeComponentSafeOnProcess;
        private Remover<(int time, IThreadSafeComponentCopyProcessor safeProcessor)> removerReturnToPool;

        public void UpdateLocal()
        {
            foreach (var safeCopy in threadSafeComponentCopyProcessors)
            {
                safeCopy.Update();

                if (safeCopy.IsComplete)
                {
                    removeComponentSafeOnProcess.Add(safeCopy);
                    returnToPool.Add((ticksToPass, safeCopy));
                }
            }

            for (int i = 0; i < returnToPool.Count; i++)
            {
                if (returnToPool.Data[i].time <= 0)
                {
                    removerReturnToPool.Add(returnToPool.Data[i]);
                    poolOfProcessors[returnToPool.Data[i].safeProcessor.TypeCode].Push(returnToPool.Data[i].safeProcessor);
                }

                returnToPool.Data[i].time--;
            }

            removeComponentSafeOnProcess.ProcessRemoving();
            removerReturnToPool.ProcessRemoving();
        }

        public ProcessCopy<T, U> GetPoolThreadSafeCopyProcessor<T, U>(U component) where T : IResolver<T,U> where U : IComponent
        {
            if (poolOfProcessors.TryGetValue(component.GetTypeHashCode, out var stack))
            {
                if (stack.TryPop(out var result))
                {
                    var needed = result as ProcessCopy<T, U>;
                    needed.Init(component);

                    threadSafeComponentCopyProcessors.Add(needed);
                    return needed;
                }
                else
                {
                    var instance = new ProcessCopy<T, U>();
                    instance.Init(component);
                    threadSafeComponentCopyProcessors.Add(instance);
                    return instance;
                }
            }
            else
            {
                poolOfProcessors.TryAdd(component.GetTypeHashCode, new Stack<IThreadSafeComponentCopyProcessor>());
                var instance = new ProcessCopy<T, U>();
                instance.Init(component);
                threadSafeComponentCopyProcessors.Add(instance);
                return instance;
            }
        }

        public void AfterEntityInit()
        {
            removeComponentSafeOnProcess = new Remover<IThreadSafeComponentCopyProcessor>(threadSafeComponentCopyProcessors);
            removerReturnToPool = new Remover<(int time, IThreadSafeComponentCopyProcessor safeProcessor)>(returnToPool);
        }
    }
}
