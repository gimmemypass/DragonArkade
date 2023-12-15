using Systems;

namespace HECSFramework.Core
{
    public partial class World
    {
        partial void AddStrategiesPart()
        {
            var strategiesController = GetEntityFromPool("Strategies Controller");
            strategiesController.AddHecsSystem(new StrategiesMainServiceSystem());
            strategiesController.AddHecsSystem(new StateUpdateSystem());
            strategiesController.Init();
        }
    }
}