using System;
using Commands;
using Components;
using HECSFramework.Core;
using HECSFramework.Unity;
using Newtonsoft.Json;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.Save, Doc.Load, "Cache ISaveable components from container and save/load them on after entity init")]
    public sealed class SaveLoadSystem : BaseSystem, IReactGlobalCommand<SaveCommand>, IAfterEntityInit
    {
        public override void InitSystem()
        {
        }

        public void CommandGlobalReact(SaveCommand command) => 
            SaveEntitiesData();

        public void AfterEntityInit() => 
            LoadEntitiesData();

        private void SaveEntitiesData()
        {
            JSONEntityContainer container = new JSONEntityContainer();
            container.SerializeEntitySavebleOnly(Owner);
            if (container.Components.Count == 0 && container.Systems.Count == 0)
            {
                HECSDebug.LogWarning($"Nothing to save on {Owner.ID}");
                return;
            }
            string json = JsonConvert.SerializeObject(container);
            string path = SavePathProvider.GetSavePath(ContainerIndex());
            SaveManager.SaveJson(path, json);
        }

        private void LoadEntitiesData()
        {
            if (SaveManager.TryLoadJson(SavePathProvider.GetSavePath(ContainerIndex()), out string json))
            {
                JSONEntityContainer container = JsonConvert.DeserializeObject<JSONEntityContainer>(json);
                container.DeserializeToEntity(Owner);
            }
        }

        private string ContainerIndex() =>
            Owner.GetComponent<ActorContainerID>().ContainerIndex.ToString();
    }
}