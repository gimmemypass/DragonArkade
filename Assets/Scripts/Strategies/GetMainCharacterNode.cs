using System;
using Components;
using HECSFramework.Core;
using Strategies;

namespace HECSFramework.Unity.Strategies
{
    [Serializable]
    public class GetMainCharacterNode : GenericNode<Entity>
    {
        public override string TitleOfNode { get; } = "Get Main Character Node";
        
        [Connection(ConnectionPointType.Out, " <Entity> Out")]
        public BaseDecisionNode Out;
        public override Entity Value(Entity entity)
        {
            return EntityManager.Default.GetSingleComponent<MainCharacterTagComponent>().Owner;
        }

        public override void Execute(Entity entity)
        {
            
        }
    }
}