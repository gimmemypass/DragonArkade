using System.Buffers;
using HECSFramework.Core;
using Systems;

namespace HECSFramework.Serialize
{
    public ref struct PooledBuffer
    {
        private ArrayBufferWriter<byte> bufferWriter;
        private bool isInited; 

        public ArrayBufferWriter<byte> GetBufferWriter()
        {
            if (!isInited)
            {
                bufferWriter = EntityManager.GetSingleSystem<PoolingSystem>().GetArrayBuffer();
                isInited = true;
            }
            return bufferWriter;
        }

        public void Release()
        {
            EntityManager.GetSingleSystem<PoolingSystem>().ReleaseArrayBuffer(bufferWriter);
        }
    }
}