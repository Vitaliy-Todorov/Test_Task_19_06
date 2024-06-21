using System;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities.Enemies.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public string ID { get; private set; }
        
        private EnemiesSpawner _enemiesSpawner;

        [Inject]
        private void Construct(EnemiesSpawner enemiesSpawner) => 
            _enemiesSpawner = enemiesSpawner;

        private void Awake() => 
            ID = Guid.NewGuid().ToString();

        private void Start() => 
            _enemiesSpawner.EnemyWasCreated(ID, this);

        private void OnDestroy() => 
            _enemiesSpawner.EnemyWasDestroyed(ID);
    }
}