using System;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;

namespace Components
{
    public sealed partial class AnimatorStateComponent : IHaveActor, IInitable, IInitAfterView, IDisposable
    {
        public Actor Actor { get; set; }
        [HideInInspectorCrossPlatform]
        public Animator Animator;
        [HideInInspectorCrossPlatform]
        public bool Activated;

        public void Init()
        {
            if (Owner.ContainsMask<ViewReferenceGameObjectComponent>())
                return;

            SetupAnimatorState();
        }

        public void SetupAnimatorState()
        {
            Actor.TryGetComponent(out Animator, true);

            if (Animator != null && Animator.runtimeAnimatorController is AnimatorOverrideController overrideController)
                State = AnimatorManager.GetAnimatorState(overrideController.runtimeAnimatorController.name);
            else
                State = AnimatorManager.GetAnimatorState(Animator.runtimeAnimatorController.name);

            State.SetAnimator(Animator);
            Activated = true;
        }

        public void SetTrigger(int id)
        {
            Animator.SetTrigger(id);
        }

        public void InitAfterView()
        {
            SetupAnimatorState();
        }

        public void Reset()
        {
            Activated = false;
            Animator = null;
        }

        public void Dispose()
        {
            Reset();
        }
    }
}