using System;
using System.Threading.Tasks;
using HECSFramework.Core;
using Systems;

namespace HECSFramework.Serialize
{
    public static class CopyExtention
    {
        public async static ValueTask<TResolver> GetThreadSafeCopy<TResolver,UComponent> (this UComponent component) where TResolver: IResolver<TResolver, UComponent> where UComponent : IComponent
        {
            if (component == null || !component.IsAlive || !component.Owner.IsAlive())
                return default (TResolver);

            var pooling = component.Owner.World.GetSingleSystem<PoolingSystem>();
            var processor = pooling.GetPoolThreadSafeCopyProcessor<TResolver, UComponent>(component);

            while (!processor.IsComplete)
                await Task.Delay(5);

            return processor.Result;
        }

        public class ProcessCopy<TResolver, UComponent> : IThreadSafeComponentCopyProcessor where TResolver : IResolver<TResolver, UComponent> where UComponent : IComponent
        {
            private UComponent component;
            private TResolver result;
            private bool isComplete;
            private int typeCode;

            public int TypeCode => typeCode;
            public bool IsComplete => isComplete;
            public Guid InstanceGuid { get; } = Guid.NewGuid();

            public void Init(UComponent uComponent)
            {
                component = uComponent;
                isComplete = false;
                typeCode = TypesMap.GetIndexByType<UComponent>();
            }

            public void Update()
            {
                result.In(ref component);
                isComplete = true;
            }

            public TResolver Result
            {
                get 
                {
                    if (!IsComplete)
                        throw new Exception("You should await until is complete before taking result");

                    return result; 
                }
            }
        }
    }

    public interface IThreadSafeComponentCopyProcessor
    {
        void Update();
        int TypeCode { get; }
        bool IsComplete { get; }
        Guid InstanceGuid { get; }
    }
}