using Components;
using HECSFramework.Core;
using System;

namespace Strategies
{
    public class SetStateNode : LogNode, IInitable
    {
        [Connection(ConnectionPointType.In, "Input")]
        public BaseDecisionNode Input;

        [Connection(ConnectionPointType.In, "Execute on Start")]
        public BaseDecisionNode OnStartStateNodes;

        [Connection(ConnectionPointType.Out, "Early Update")]
        public BaseDecisionNode EarlyUpdateNodes;

        [Connection(ConnectionPointType.Out, "Exit")]
        public BaseDecisionNode Exit;

        [UnityEngine.SerializeField]
        public State State;

        //тут мы выставляем стейт если запускаем этот стейт из стейта, чтобы мы могли вернуться в текущий стейт при выходе из того стейта что запустим
        [NonSerialized] public (bool weHaveFromState, State FromState) ExternalState; 

        public override string TitleOfNode { get; } = "Set State";

        private readonly HECSMask stateContextMask = HMasks.GetMask<StateContextComponent>(); 

        public override void Execute(Entity entity)
        {
           Run(entity);
        }

        public void Init()
        {
            State.Init();
        }

        protected override void Run(Entity entity)
        {
            OnStartStateNodes?.Execute(entity);
            var context = entity.GetOrAddComponent<StateContextComponent>();
            context.EarlyUpdateNode = EarlyUpdateNodes;
            State.Execute(entity, Exit);
        }
    }
}
