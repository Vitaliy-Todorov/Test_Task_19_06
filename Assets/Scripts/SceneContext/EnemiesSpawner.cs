using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Entities.Enemies.Enemies;
using Entities.HP;
using Infrastructure.DataServiceNamespace;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using UnityEngine;
using Zenject;

namespace SceneContext
{
    public class EnemiesSpawner
    {
        public Action<EnemyData> OnEnemyWasDestroyed;
        
        public Dictionary<string, Enemy> Enemies { get; }
        
        private float _lastWaveStartTime;

        private DiContainer _diContainer;
        private StaticDataService _staticDataService;
        private DataService _dataService;
        private WaveController _waveController;
        private TimeController _timeController;
        private GameModelStaticData _gameModelStaticData;

        private EnemiesSpawner(DiContainer diContainer,
            StaticDataService staticDataService,
            DataService dataService,
            WaveController waveController,
            TimeController timeController)
        {
            _staticDataService = staticDataService;
            _diContainer = diContainer;
            _dataService = dataService;
            _waveController = waveController;
            _timeController = timeController;

            Enemies = new Dictionary<string, Enemy>();

            _waveController.OnWaveStart += WaveStart;
            
            _gameModelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
        }

        public void ClearAllEnemies()
        {
            foreach (KeyValuePair<string,Enemy> enemy in Enemies) 
                GameObject.Destroy(enemy.Value.gameObject);
            Enemies.Clear();
        }
        
        public void EnemyWasDestroyed(string id)
        {
            if(!Enemies.TryGetValue(id, out var enemy))
                return;
            EnemyData enemyData = enemy.EnemyData;
            Enemies.Remove(id);
            OnEnemyWasDestroyed?.Invoke(enemyData);
        }

        public void EnemyWasCreated(string id, Enemy enemy) => 
            Enemies.Add(id, enemy);

        private void WaveStart(int waveNumber)
        {
            float totalHPLevel = (float) _gameModelStaticData.MaxHealth * _waveController.WavesCount / 200;
            if(waveNumber % _gameModelStaticData.WaveWithBoss == 0)
            {
                GameObject enemyGO = _diContainer.InstantiatePrefab(_staticDataService.GetEntityStaticData(EntityType.Boss).Prefab, _gameModelStaticData.EnemiesSpawnPoint, Quaternion.identity);
                enemyGO.GetComponent<Health>().SetMaxHealth(totalHPLevel);
            }
            else
                Spawn(_gameModelStaticData.EnemiesCount, _gameModelStaticData.EnemiesSpawnPoint, totalHPLevel, _gameModelStaticData.TimeBetweenSpawn).Forget();
        }

        private async UniTask Spawn(int enemiesCount, Vector3 spawnPoints, float totalHpLevel, float timeBetweenSpawn = 0)
        {
            float hpEnemy = totalHpLevel / enemiesCount;
            while (enemiesCount > 0)
            {
                enemiesCount--;
                GameObject enemyGO = _diContainer.InstantiatePrefab(_staticDataService.GetEntityStaticData(EntityType.Enemy).Prefab, spawnPoints, Quaternion.identity);
                enemyGO.GetComponent<Health>().SetMaxHealth(hpEnemy);

                _lastWaveStartTime = _timeController.CurrentTime;
                await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= timeBetweenSpawn);
            }
        }
    }
}