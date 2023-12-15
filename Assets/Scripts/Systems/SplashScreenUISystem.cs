using System;
using Commands;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;
using Components;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class SplashScreenUISystem : BaseSystem, IUpdatable
    {
        [Required] public SplashScreenUIComponent SplashScreenUIComponent;
        public override void InitSystem()
        {
            
        }

        public void UpdateLocal()
        {
            SplashScreenUIComponent.MinShowTime -= Time.deltaTime;
            if(SplashScreenUIComponent.MinShowTime < 0)
                EntityManager.Default.Command(new DestroyEntityWorldCommand(){Entity = Owner});
        }
    }
}