using System.Runtime.CompilerServices;
using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, Doc.UniversalNodes, Doc.HECS, "this node get ability entity from entity")]
    public class GetAbilityNode : GenericNode<Entity>
    {
        [Connection(ConnectionPointType.In, "<Entity> Additional Entity")]
        public GenericNode<Entity> AdditionalEntity;

        public override string TitleOfNode { get; } = "GetAbilityNode";

        [Connection(ConnectionPointType.Out, "<Entity> Out")]
        public BaseDecisionNode Out;

        [AbilityIDDropDown]
        public int AbilityIndex;

        public override void Execute(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public override Entity Value(Entity entity)
        {
            if (AdditionalEntity != null)
                return GetAbility(AdditionalEntity.Value(entity));
            else
                return GetAbility(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Entity GetAbility(Entity entity)
        {
            if (entity.TryGetComponent(out AbilitiesHolderComponent abilitiesHolderComponent))
            {
                if (abilitiesHolderComponent.IndexToAbility.TryGetValue(AbilityIndex, out var ability))
                {
                    return ability;
                }
            }

            return default;
        }
    }
}
