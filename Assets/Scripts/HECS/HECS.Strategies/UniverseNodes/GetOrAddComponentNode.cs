using System.Runtime.CompilerServices;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.HECS, Doc.Strategy, Doc.UniversalNodes, "this node get or add component to entity by index")]
    public sealed class GetOrAddComponentNode : InterDecision
    {
        [ComponentMaskDropDown]
        public int ComponentIndex;

        public override string TitleOfNode { get; } = "GetOrAddComponentNode";

        [Connection(ConnectionPointType.In, "<Entity> Optional Entity")]
        public GenericNode<Entity> AdditionalEntity;

        protected override void Run(Entity entity)
        {
            if (AdditionalEntity != null)
                AddComponentToEntity(AdditionalEntity.Value(entity));
            else
                AddComponentToEntity(entity);

            Next.Execute(entity); ;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddComponentToEntity(Entity entity)
        {
            if (entity.ContainsMask(ComponentIndex))
                return;

           entity.AddComponent(TypesMap.GetComponentFromFactory(ComponentIndex));
        }
    }
}
