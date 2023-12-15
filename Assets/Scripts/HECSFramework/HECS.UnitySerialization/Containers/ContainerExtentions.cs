using System;
using HECSFramework.Core;
using HECSFramework.Serialize;

namespace HECSFramework.Unity
{
    public static class ContainerExtentions
    {
        [Obsolete]
        public static void AddComponentsToFastEntity(this EntityContainer entityContainer, FastEntity fastEntity)
        {
            foreach (var c in entityContainer.Components)
            {
                HECSComponentToFastComponent.AddComponentToFastEntity(c.GetHECSComponent, fastEntity);
            }
        }

        [Obsolete]
        public static FastEntity GetFastEntityFromContainer(this EntityContainer entityContainer, World world)
        {
            ref var fastEntity = ref FastEntity.Get(world);

            foreach (var c in entityContainer.Components)
            {
                HECSComponentToFastComponent.AddComponentToFastEntity(c.GetHECSComponent, fastEntity);
            }

            return fastEntity;
        }
    }
}
