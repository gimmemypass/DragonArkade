using Components;
using HECSFramework.Core;

namespace Systems
{
    [Documentation(Doc.GameLogic, Doc.AI, Doc.Strategy, "Эта система живет  мире и добавляется через парт часть гейм контроллера, она отвечает за поддержку глобальной логики для стратегий")]
    public class StrategiesMainServiceSystem : BaseSystem
    {
        private EntitiesFilter stackInfos;

        public override void InitSystem()
        {
            stackInfos = Owner.World.GetFilter<StateInfoComponent>();

#if UNITY_EDITOR
            Owner.World.GlobalUpdateSystem.FinishUpdate += React;
#endif
        }

#if UNITY_EDITOR
        private void React()
        {
           foreach (var entity in stackInfos)
           {
               var info = entity.GetComponent<StateInfoComponent>();

               if (info.NeedInfo)
               {
                   if (info.NeedClean)
                   {
                       info.StateStack.Clear();
                       info.CurrentCycle = 0;
                       info.NeedClean = false;
                   }

                   if (info.CurrentCycle > info.MaxCycles)
                       info.NeedInfo = false;

                   info.CurrentCycle++;
               }
           }
        }
#endif
    }
}
