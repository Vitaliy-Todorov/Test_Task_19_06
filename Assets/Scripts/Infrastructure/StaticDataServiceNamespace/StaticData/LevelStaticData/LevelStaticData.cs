using System.Collections.Generic;
using UnityEngine;

namespace ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData
{
    [CreateAssetMenu(menuName = "StaticData/Level", fileName = "LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public LevelName LevelName { get; private set; }

        [field: SerializeField, Header("Wave")] public double TimeStartWaves { private set; get; }
        [field: SerializeField] public double TimeBetweenWaves { get; private set; }
        [field: SerializeField] public int WavesCount { get; private set; }


        [field: SerializeField, Header("EnemiesSpawner")] public float TimeBetweenSpawn { get; private set; }
        [field: SerializeField] public int EnemiesCount { get; private set; }
        [field: SerializeField] public List<Vector3> EnemiesSpawnPoints { get; private set; }
    }
}