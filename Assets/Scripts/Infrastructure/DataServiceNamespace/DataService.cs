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
        public int BuildingCost => _gameModelStaticData.BuildingCost;
        private GameModelStaticData _gameModelStaticData;
        private BuildingsSpawner _buildingsSpawner;
        private TimeController _timeController;

        public DataService(StaticDataService staticDataService,
            BuildingsSpawner buildingsSpawner,
            TimeController timeController)
        {
            _timeController = timeController;
            _buildingsSpawner = buildingsSpawner;
            
            _gameModelStaticData = staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
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
            return timeSpan.Minutes + _gameModelStaticData.FirstLaunchDate;
        }
    }
}
