using System;
using System.Collections.Generic;
using Commands;
using Components;
using HECSFramework.Unity;
using HECSFramework.Core;
using Components.MonoBehaviourComponents;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.NONE, "")]
    public sealed class LevelBattleUISystem : BaseSystem, IHaveActor, IReactGlobalCommand<TransitionGameStateCommand>
    {
        public Actor Actor { get; set; }
        private LevelBattleUIMonoComponent monoComponent;
        private PlayerLevelComponent playerLevelComponent;
        private LevelsHolderComponent levelsHolderComponent;

        public List<WaveUIMonoComponent> waves = new();
        public override void InitSystem()
        {
            Actor.TryGetComponent(out monoComponent);
            playerLevelComponent = EntityManager.GetSingleComponent<PlayerLevelComponent>();
            levelsHolderComponent = EntityManager.GetSingleComponent<LevelsHolderComponent>();
            UpdateVisual();
        }

        public void CommandGlobalReact(TransitionGameStateCommand command)
        {
            UpdateVisual(); 
        }

        private void UpdateVisual()
        {
            monoComponent.LevelText.text = $"Level {(playerLevelComponent.Level + 1).ToString()}";
            var maxWaves = levelsHolderComponent.LevelDatas[playerLevelComponent.Level].Waves.Length;
            while (maxWaves - waves.Count > 0)
            {
                var wave = Object.Instantiate(monoComponent.WavePrefab, monoComponent.WavesParent);
                // wave.transform.SetSiblingIndex(0);
                wave.gameObject.SetActive(true);
                waves.Add(wave);
            }

            for (var i = 0; i < waves.Count; i++)
            {
                var wave = waves[i];
                wave.SetActiveWave(i < playerLevelComponent.WaveNumber);
            }
        }

    }
}