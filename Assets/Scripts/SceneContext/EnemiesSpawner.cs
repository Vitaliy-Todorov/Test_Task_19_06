using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities.Enemies.Enemies;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace SceneContext
{
    public class EnemiesSpawner
    {
        public Dictionary<string, Enemy> Enemies { get; }
        
        private float _lastWaveStartTime;

        private DiContainer _diContainer;
        private StaticDataService _staticDataService;
        private WaveController _waveController;
        private TimeController _timeController;

        private EnemiesSpawner(DiContainer diContainer, StaticDataService staticDataService, WaveController waveController, TimeController timeController)
        {
            _staticDataService = staticDataService;
            _diContainer = diContainer;
            _waveController = waveController;
            _timeController = timeController;

            Enemies = new Dictionary<string, Enemy>();

            _waveController.OnWaveStart += WaveStart;
        }

        private void WaveStart()
        {
            LevelStaticData levelStaticData = _staticDataService.GetLevelStaticData(LevelName.LevelTest);
            Spawn(levelStaticData.EnemiesCount, levelStaticData.EnemiesSpawnPoints, levelStaticData.TimeBetweenSpawn).Forget();
        }

        public async UniTask Spawn(int enemiesCount, List<Vector3> spawnPoints, float timeBetweenSpawn = 0)
        {
            while (enemiesCount > 0)
            {
                enemiesCount--;
                int randomIndex = Random.Range(0, spawnPoints.Count - 1);
                _diContainer.InstantiatePrefab(_staticDataService.GetEntityStaticData(EntityType.Enemy).Prefab, spawnPoints[randomIndex], Quaternion.identity);
                
                
                _lastWaveStartTime = _timeController.CurrentTime;
                await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= timeBetweenSpawn);
            }
        }

        public void EnemyWasDestroyed(string id)
        {
            Enemies.Remove(id);
            // OnChangingBuildingsPositions?.Invoke();
        }

        public void EnemyWasCreated(string id, Enemy building)
        {
            Enemies.Add(id, building);
            // OnChangingBuildingsPositions?.Invoke();
        }
    }
}