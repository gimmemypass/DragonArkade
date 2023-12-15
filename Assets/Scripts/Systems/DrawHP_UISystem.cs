using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;
using Components.MonoBehaviourComponents;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class DrawHP_UISystem : BaseSystem, IHaveActor, IReactCommand<RedrawBarCommand<float>>, IReactCommand<SetColorCommand>
    {
        public Actor Actor { get; set; }
        private HealthBarMonoComponent hPBarMonoComponent;

        public override void InitSystem()
        {
            Actor.TryGetComponent(out hPBarMonoComponent, true);
        }

        public void CommandReact(RedrawBarCommand<float> command)
        {
            hPBarMonoComponent.SetProgress(command.CurrentValue, command.MaxValue);
        }

        public void CommandReact(SetColorCommand command)
        {
            hPBarMonoComponent.SetColor(command.Color);
        }
    }
}