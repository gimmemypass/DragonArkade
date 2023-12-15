using Components;
using HECSFramework.Core;

namespace Strategies
{
    [Documentation(Doc.Strategy, "Это базовая нода, она логирует в отдельный компонент ноды по которым мы проходимся в стратегии")]
    public abstract class LogNode : BaseDecisionNode
    {
        public override void Execute(Entity entity)
        {
#if UNITY_EDITOR
            var info = entity.GetOrAddComponent<StateInfoComponent>();

            if (info.NeedInfo)
                info.StateStack.Add(this);
#endif
            Run(entity);
        }

        protected abstract void Run(Entity entity);
    }
}