using System;
using Entities.Enemies.Enemies;

namespace SceneContext
{
    public class Counter
    {
        public Action<int> OnScoreChanged;
        private int _score;
        
        public Counter(EnemiesSpawner enemiesSpawner)
        {
            enemiesSpawner.OnEnemyWasDestroyed += EnemyWasDestroyed;
        }

        private void EnemyWasDestroyed(EnemyData enemyData)
        {
            _score += enemyData.PointsForDestruction;
            OnScoreChanged?.Invoke(_score);
        }
    }
}