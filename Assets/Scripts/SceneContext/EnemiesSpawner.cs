using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities.Enemies.Enemies;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
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
        public Action<EnemyData> OnEnemyWasDestroyed;
        
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

        private void WaveStart(int waveNumber)
        {
            GameModelStaticData gameModelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
            if(waveNumber % gameModelStaticData.WaveWithBoss == 0)
                _diContainer.InstantiatePrefab(_staticDataService.GetEntityStaticData(EntityType.Boss).Prefab, gameModelStaticData.EnemiesSpawnPoint, Quaternion.identity);
            else
                Spawn(gameModelStaticData.EnemiesCount, gameModelStaticData.EnemiesSpawnPoint, gameModelStaticData.TimeBetweenSpawn).Forget();
        }

        private async UniTask Spawn(int enemiesCount, Vector3 spawnPoints, float timeBetweenSpawn = 0)
        {
            while (enemiesCount > 0)
            {
                enemiesCount--;
                _diContainer.InstantiatePrefab(_staticDataService.GetEntityStaticData(EntityType.Enemy).Prefab, spawnPoints, Quaternion.identity);
                
                _lastWaveStartTime = _timeController.CurrentTime;
                await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= timeBetweenSpawn);
            }
        }

        public void EnemyWasDestroyed(string id)
        {
            EnemyData enemyData = Enemies[id].EnemyData;
            Enemies.Remove(id);
            OnEnemyWasDestroyed?.Invoke(enemyData);
        }

        public void EnemyWasCreated(string id, Enemy enemy) => 
            Enemies.Add(id, enemy);
    }
}