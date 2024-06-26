using HECSFramework.Core;
using HECSFramework.Core.Helpers;
using Sirenix.OdinInspector;
using Strategies;
using System;
using System.Collections.Generic;

namespace Components
{
    [Serializable, Documentation(Doc.AI, Doc.Strategy, Doc.State, "Это основной компонент стейта, он содержит сущности которые сейчас находятся в этом стейте, этот компонент лежит внутри ентити внутри " + nameof(State))]
    public class StateDataComponent : BaseComponent, IInitable 
    {
        private List<Entity> entitiesInCurrentState;
        public ReadonlyList<Entity> EntitiesInCurrentState;
        private Queue<Entity> addQueue = new Queue<Entity>(256);
        private Queue<Entity> removeQueue = new Queue<Entity>(256);
        private List<Entity> onPause = new List<Entity>(256);
        public StrategyState State { get; private set; } = StrategyState.Run;
        [ShowInInspector, ReadOnly] public string OwnerID => Owner.ID;

        public void Init()
        {
            entitiesInCurrentState = new List<Entity>();
            EntitiesInCurrentState = new ReadonlyList<Entity>(entitiesInCurrentState);
        }

        public void AddToState(Entity entity)
        {
            addQueue.Enqueue(entity);
        }

        public void RemoveFromState(Entity entity)
        {
            removeQueue.Enqueue(entity);
        }

        public void ChangeState(StrategyState state)
        {
            State = state;
        }

        public void UpdateCollection()
        {
            while (removeQueue.Count > 0)
                entitiesInCurrentState.Remove(removeQueue.Dequeue());

            while (addQueue.Count > 0)
                entitiesInCurrentState.AddUniqueElement(addQueue.Dequeue());
        }

        public void Pause(Entity entity)
        {
            if (entitiesInCurrentState.Remove(entity))
                onPause.Add(entity);
        }

        public void UnPause(Entity entity)
        {
            if (onPause.Remove(entity))
                entitiesInCurrentState.Add(entity);
        }
    }
}