using Components;
using HECSFramework.Core;
using Strategies;

namespace Systems
{
    [Documentation(Doc.AI, Doc.Strategy, Doc.State, Doc.HECS, "это глобальная система которая отвечает за апдейт состояний")]
    public class StateUpdateSystem : BaseSystem, IUpdatable
    {
        private EntitiesFilter statesEntities;

        public override void InitSystem()
        {
            statesEntities = Owner.World.GetFilter<StateContextComponent>();
        }

        public void UpdateLocal()
        {
            foreach (var needed in statesEntities)
            {
                if (needed.TryGetComponent(out StateContextComponent stateContextComponent))
                {
                    if (stateContextComponent.StrategyState != StrategyState.Run) continue;

                    var generation = stateContextComponent.CurrentIteration;
                    stateContextComponent.EarlyUpdateNode?.Execute(needed);

                    if (generation != stateContextComponent.CurrentIteration)
                        continue;

                    stateContextComponent.CurrentState.Update.Execute(needed);
                }
            }
        }
    }
}