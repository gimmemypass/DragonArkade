using System;
using System.Linq;
using Components;
using HECSFramework.Core;

#if UNITY_EDITOR || UNITY_2017_1_OR_NEWER
using UnityEngine;
#endif

namespace Strategies
{
#if UNITY_EDITOR || UNITY_2017_1_OR_NEWER
    [CreateAssetMenu(menuName = "Strategies/State")]
#endif
    [Documentation(Doc.Strategy, Doc.AI, "Это подвид стратегии - FSM")]
    public partial class State : BaseStrategy, IState, IInitable, IDecisionNode
    {
        public UpdateStateNode Update { get; private set; }
        public StartDecision StartDecision { get; private set; }

        public override void Init()
        {
            if (isInited) return;

            InitNodes();
        }

        private void InitNodes()
        {
            if (isInited) return;

            isInited = true;

            StartDecision = nodes.FirstOrDefault(x => x is StartDecision) as StartDecision;
            Update = nodes.FirstOrDefault(x => x is UpdateStateNode) as UpdateStateNode;
            var addState = nodes.OfType<IAddStateNode>();

            foreach (var n in addState)
                n.AddState(this);

            var setStateNodes = nodes.OfType<SetStateNode>();

            foreach (var n in setStateNodes)
                n.ExternalState = (true, this);


            var initable = nodes.OfType<IInitable>();

            foreach (var n in initable)
                n.Init();
        }

        public void Pause(Entity pause)
        {
            pause.GetComponent<StateContextComponent>().StrategyState = StrategyState.Pause;
        }

        public void Stop(Entity entity)
        {
            entity.GetComponent<StateContextComponent>().StrategyState = StrategyState.Stop;
        }

        public void UnPause(Entity entity)
        {
            entity.GetComponent<StateContextComponent>().StrategyState = StrategyState.Run;
        }

        public override void Execute(Entity entity)
        {
            StartDecision?.Execute(entity);
        }

        public void Execute(Entity entity, IDecisionNode exitNode)
        {
            var context = entity.GetOrAddComponent<StateContextComponent>();
            context.CurrentState = this;
            context.ExitStateNodes.Push(exitNode);
            context.StrategyState = StrategyState.Run;
            Execute(entity);
        }
    }

    public interface IState
    {
        void Execute(Entity entity);
        void Pause(Entity entity);
        void UnPause(Entity entity);
        void Stop(Entity entity);
    }

    public interface IAddStateNode
    {
        void AddState(State state);
    }

    public enum StrategyState { Run, Pause, Stop }
}