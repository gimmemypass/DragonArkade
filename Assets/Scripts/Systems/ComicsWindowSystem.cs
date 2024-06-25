using System;
using Components;
using Components.MonoBehaviourComponents;
using HECSFramework.Unity;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.UI, "window system for comics")]
    public sealed class ComicsWindowSystem : BaseSystem, IHaveActor, IGlobalStart
    {
        public Actor Actor { get; set; }
        
        private ComicsComponent component;
        private ComicsMonoComponent monoComponent;
        private int currentPage;
        public override void InitSystem()
        {
            component = Owner.GetComponent<ComicsComponent>();
            Actor.TryGetComponent(out monoComponent);
            monoComponent.OnClick += MoveNext;
        }

        public override void Dispose()
        {
            base.Dispose();
            monoComponent.OnClick -= MoveNext;
        }

        public void GlobalStart()
        {
            MoveNext();
        }

        private void MoveNext()
        {
            if (currentPage >= component.Sprites.Length)
            {
                Owner.HecsDestroy();
                return;
            }
            monoComponent.SetSprite(component.Sprites[currentPage++]); 
        }
    }
}