using System;
using System.Collections.Generic;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using UnityEngine;

namespace Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData
{
    [CreateAssetMenu(menuName = "StaticData/GameModel", fileName = "GameModelStaticData")]
    public class GameModelStaticData : ScriptableObject
    {
        [field: SerializeField] public GameModelName GameModelName { get; private set; }

        [field: SerializeField, Space] public float MaxTime { get; private set; } = 115200;
        [field: SerializeField, Range(0,400)] public int StartWavesCount { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; } = 220400;
        [field: SerializeField] public float MaxLevel { get; private set; } = 200;
        [field: SerializeField] public float BoosMaxLevel { get; private set; } = 400;


        [field: SerializeField, Header("Wave")] public double TimeStartWaves { get; private set; }
        [field: SerializeField] public double TimeBetweenWaves { get; private set; }

        [field: SerializeField, Header("EnemiesSpawner")] public int WaveWithBoss { private set; get; }
        [field: SerializeField, Space] public float TimeBetweenSpawn { get; private set; }
        [field: SerializeField] public int EnemiesCount { get; private set; }
        [field: SerializeField] public Vector3 EnemiesSpawnPoint { get; private set; }
        [field: SerializeField] public int BuildingCost { get; private set; }
    }
}