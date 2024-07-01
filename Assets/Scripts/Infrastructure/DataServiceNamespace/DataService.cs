using System;
using System.Collections.Generic;
using Entities.Building;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using SceneContext;
using UnityEngine;

namespace Infrastructure.DataServiceNamespace
{
    public class DataService
    {
        public float CurrentTime { get; set; }
        public bool Boost { get; set; }
        public int BuildingCost => _gameModelStaticData.BuildingCost;

        public float MaxLevel
        {
            get
            {
                if (!Boost)
                    return _gameModelStaticData.MaxLevel;
                else
                    return _gameModelStaticData.BoosMaxLevel;
            }
        }

        private const string BoostKey = "Boost";
        private const string CurrentTimeKey = "CurrentTimeKey";
        private const string CurrentLevelKey = "CurrentLevelKey";

        private GameModelStaticData _gameModelStaticData;
        private BuildingsSpawner _buildingsSpawner;
        private TimeController _timeController;
        private WaveController _waveController;


        public DataService(StaticDataService staticDataService,
            BuildingsSpawner buildingsSpawner,
            TimeController timeController,
            WaveController waveController)
        {
            _timeController = timeController;
            _buildingsSpawner = buildingsSpawner;
            _waveController = waveController;

            _gameModelStaticData = staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);

            Load();
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey(BoostKey))
            {
                Boost = "true" == PlayerPrefs.GetString(BoostKey);
                Debug.Log($"Load Boost: {Boost}");
            }
            else
            {
                PlayerPrefs.SetString(BoostKey, Boost.ToString());
                Debug.Log($"Load Boost: {Boost}");
            }
            
            if (PlayerPrefs.HasKey(CurrentTimeKey))
            {
                CurrentTime = PlayerPrefs.GetFloat(CurrentTimeKey);
                Debug.Log($"Load Boost: {Boost}");
            }
            else
            {
                PlayerPrefs.SetFloat(CurrentTimeKey, CurrentTime);
                Debug.Log($"Load Boost: {CurrentTimeKey}");
            }
            
            if (PlayerPrefs.HasKey(CurrentLevelKey))
            {
                int currentLevel = PlayerPrefs.GetInt(CurrentLevelKey);
                _waveController.SetWavesCount(currentLevel);
                Debug.Log($"Load Boost: {Boost}");
            }
            else
            {
                PlayerPrefs.SetInt(CurrentLevelKey, _waveController.WavesCount);
                Debug.Log($"Load Boost: {CurrentLevelKey}");
            }
        }

        public float TotalHPLevel() => 
            40 * TimeInMinutes();

        public float DPSOneLevelBuilding()
        {
            int totalLevel = 0;
            
            foreach (KeyValuePair<string,Building> building in _buildingsSpawner.Buildings)
                totalLevel += building.Value.Level;

            float totalDPS = (float) 2 / 3 * TimeInMinutes();
            return totalDPS / totalLevel;
        }

        private float TimeInMinutes()
        {
            /*TimeSpan timeSpan = DateTime.Now - _timeController.FirstLaunchDate;
            return timeSpan.Minutes;*/
            TimeSpan timeSpan = DateTime.Now - _timeController.FirstLaunchDate;
            return timeSpan.Minutes + CurrentTime;
        }
    }
}
