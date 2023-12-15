using System;
using Components.MonoBehaviourComponents;
using HECSFramework.Unity;
using HECSFramework.Core;
using HECSFramework.Serialize;
using UnityEngine;

namespace Components
{
    [Serializable]
    [JSONHECSSerialize]
    [Documentation(Doc.NONE, "")]
    public sealed class PlayerLevelComponent : BaseComponent, IWorldSingleComponent, ISavebleComponent
    {
        [Field(0)]
        public int Level;
        public int WaveNumber;
        public WaveMonoComponent CurrentWave;
    }
}