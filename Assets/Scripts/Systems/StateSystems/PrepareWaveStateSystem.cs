using System;
using Commands;
using Components;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using HECSFramework.Core;
using HECSFramework.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameState, "")]
    public sealed class PrepareWaveStateSystem : BaseGameStateSystem, IGlobalStart
    {
        protected override int State { get; } = GameStateIdentifierMap.PrepareWave;
        private GameStateComponent gameStateComponent;
        private PlayerLevelComponent playerLevelComponent;
        private LevelsHolderComponent levelsHolderComponent;
        private EntitiesFilter enemies;
        private EntitiesFilter spheres;
        private EntitiesFilter spawnPointes;

        public override void InitSystem()
        {
            enemies = EntityManager.Default.GetFilter<EnemyTagComponent>();
            spheres = EntityManager.Default.GetFilter<SphereComponent>();
            spawnPointes = EntityManager.Default.GetFilter<SpawnPointComponent>();
        }

        public void GlobalStart()
        {
            AsSingle(ref gameStateComponent);
            AsSingle(ref playerLevelComponent);
            AsSingle(ref levelsHolderComponent);
        }

        protected override async void ProcessState(int from, int to)
        {
            if (playerLevelComponent.CurrentWave != null)
            {
                await playerLevelComponent.CurrentWave.Finish();
                Object.Destroy(playerLevelComponent.CurrentWave.gameObject);
            }
            var wavePrefab = levelsHolderComponent.LevelDatas[playerLevelComponent.Level].Waves[playerLevelComponent.WaveNumber];
            var wave = Object.Instantiate(wavePrefab);
            foreach (var actor in wave.GetComponentsInChildren<Actor>())
            {
                actor.InitWithContainer(); 
            }
            await wave.Prepare();
            enemies.ForceUpdateFilter();
            foreach (var enemy in enemies)
            {
                enemy.Command(new ForceStartAICommand());
            }
            playerLevelComponent.CurrentWave = wave;

            HandleSpheres();
            EndState();
        }

        private void HandleSpheres()
        {
            //todo rewrite
            if (spheres.Count == 0)
                return;
            var first = true;
            foreach (var sphere in spheres)
            {
                if (first)
                {
                    first = false;
                    continue;
                }

                sphere.GetComponent<UnityTransformComponent>().Transform.DOScale(0f, 0.5f).OnComplete(() =>
                {
                    EntityManager.Default.Command(new DestroyEntityWorldCommand() { Entity = sphere });
                });
            }

            foreach (var spawnPoint in spawnPointes)
            {
                if (spawnPoint.GetComponent<SpawnPointComponent>().SpawnPointIdentifier.Id ==
                    SpawnPointIdentifierMap.SphereSpawnPointIdentifier)
                {
                    var sphere = spheres.FirstOrDefault();
                    var transform = sphere.GetComponent<UnityTransformComponent>().Transform;
                    transform.DOScale(0f, 0.25f).OnComplete(() =>
                    {
                        transform.position = spawnPoint.GetComponent<UnityTransformComponent>().Transform.position;
                        transform.DOScale(1f, 0.25f);
                    });
                    sphere.GetComponent<SphereComponent>().Dir = Vector3.zero;
                    break;
                } 
            }
        }
    }
}