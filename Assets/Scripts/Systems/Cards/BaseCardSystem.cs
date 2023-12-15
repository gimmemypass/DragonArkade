using Commands;
using Components.MonoBehaviourComponents;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Systems
{
    public abstract class BaseCardSystem : BaseSystem, IHaveActor, IUpdatable
    {
        public Actor Actor { get; set; }
        private CardMonoComponent monoComponent;
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
            monoComponent.button.onClick.AddListener(OnClick);
        }

        public override void Dispose()
        {
            monoComponent.button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (!CanExecute())
                return;
            Execute();
            EntityManager.Default.Command(new CardActivatedCommand(){Entity = Owner});
        }

        public void UpdateLocal()
        {
            //each 10 frame
            if(Time.frameCount % 10 == 0)
                monoComponent.button.interactable = CanExecute();
        }

        protected abstract void Execute();
        protected abstract bool CanExecute();
    }
}