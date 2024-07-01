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
        public bool IsRealTime { get; set; }
        public float CurrentTime { private get; set; }
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

        private const string IsRealTimeKey = "IsRealTime";
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

        public void Save()
        {
            PlayerPrefs.SetString(IsRealTimeKey, IsRealTime.ToString());
            PlayerPrefs.SetString(BoostKey, Boost.ToString());
            PlayerPrefs.SetFloat(CurrentTimeKey, CurrentTime);
            PlayerPrefs.SetInt(CurrentLevelKey, _waveController.WavesCount);
        }
        
        private void Load()
        {
            if (PlayerPrefs.HasKey(IsRealTimeKey)) 
                IsRealTime = "True" == PlayerPrefs.GetString(IsRealTimeKey);
            
            if (PlayerPrefs.HasKey(BoostKey)) 
                Boost = "True" == PlayerPrefs.GetString(BoostKey);
            
            if (PlayerPrefs.HasKey(CurrentTimeKey)) 
                CurrentTime = PlayerPrefs.GetFloat(CurrentTimeKey);
            
            if (PlayerPrefs.HasKey(CurrentLevelKey))
            {
                int currentLevel = PlayerPrefs.GetInt(CurrentLevelKey);
                _waveController.SetWavesCount(currentLevel);
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

        public float TimeInMinutes()
        {
            TimeSpan timeSpan = DateTime.Now - _timeController.FirstLaunchDate;
            if (IsRealTime)
                return timeSpan.Minutes;
            else
                return CurrentTime;
        }
    }
}
