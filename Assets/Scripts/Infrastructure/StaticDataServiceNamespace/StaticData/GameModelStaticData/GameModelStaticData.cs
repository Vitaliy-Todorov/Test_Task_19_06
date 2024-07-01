﻿using System;
using System.Collections.Generic;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using UnityEngine;

namespace Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData
{
    [CreateAssetMenu(menuName = "StaticData/GameModel", fileName = "GameModelStaticData")]
    public class GameModelStaticData : ScriptableObject
    {
        [field: SerializeField] public GameModelName GameModelName { get; private set; }
        [field: SerializeField, Range(0,115200), Space] public float FirstLaunchDate { get; private set; }
        [field: SerializeField, Range(0,200)] public int StartWavesCount { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; } = 220400;


        [field: SerializeField, Header("Wave")] public double TimeStartWaves { get; private set; }
        [field: SerializeField] public double TimeBetweenWaves { get; private set; }

        [field: SerializeField, Header("EnemiesSpawner")] public int WaveWithBoss { private set; get; }
        [field: SerializeField, Space] public float TimeBetweenSpawn { get; private set; }
        [field: SerializeField] public int EnemiesCount { get; private set; }
        [field: SerializeField] public Vector3 EnemiesSpawnPoint { get; private set; }
        [field: SerializeField] public int BuildingCost { get; private set; }
    }
}