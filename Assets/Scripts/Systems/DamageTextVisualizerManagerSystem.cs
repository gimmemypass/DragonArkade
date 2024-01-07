using System;
using Commands;
using HECSFramework.Core;
using Components;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.UI, Doc.Global, "")]
    public sealed class DamageTextVisualizerManagerSystem : BaseSystem, IReactGlobalCommand<DamageForVisualFXCommand>
    {
        private MainCameraComponent mainCameraComponent;
        public override void InitSystem()
        {
        }

        public void CommandGlobalReact(DamageForVisualFXCommand command)
        {
            if (!command.Victim.ContainsMask<NeedDefaultDamageTextVisualizerComponent>())
                return;
            //todo make pooling
            CreateVisualizer(command);
        }

        private void CreateVisualizer(DamageForVisualFXCommand command)
        {
             EntityManager.Command(new ShowUICommand()
             {
                 UIViewType = UIIdentifierMap.DamageTextVisualizer_UIIdentifier,
                 OnUILoad = ui => { Visualize(ui, command);},
                 MultyView = true
             });
        }

        private void Visualize(Entity ui, DamageForVisualFXCommand command)
        {
            ui.Command(command); 
        }
    }
}