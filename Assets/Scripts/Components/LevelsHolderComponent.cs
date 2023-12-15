using System;
using System.Collections;
using System.Collections.Generic;
using Components.MonoBehaviourComponents;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class LevelsHolderComponent : BaseComponent, IWorldSingleComponent
    {
        public int ArenaWinReward = 500;
        public int ArenaLoseReward = 100;
        
        [SerializeField]
        private LevelData[] levelDatas;
        public IReadOnlyList<LevelData> LevelDatas => levelDatas;
    }

    [Serializable]
    public struct LevelData
    {
        public int WinReward;
        public int LoseReward;
        public GameObject Location;
        public WaveMonoComponent[] Waves;
    }
}