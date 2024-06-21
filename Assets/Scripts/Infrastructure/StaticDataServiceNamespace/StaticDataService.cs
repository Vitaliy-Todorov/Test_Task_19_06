using System.Collections.Generic;
using System.Linq;
using ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using UnityEngine;

namespace ProjectContext.StaticDataServiceNamespace
{
    public class StaticDataService
    {
        private Dictionary<LevelName, LevelStaticData> _levelStaticDataDictionary;
        private Dictionary<EntityType, EntityStaticData> _entityStaticDataDictionary;

        private string LevelStaticDataPath = "StaticData/Level";
        private string EntityStaticDataPath = "StaticData/Entities";

        public StaticDataService()
        {
            _levelStaticDataDictionary = Resources.LoadAll<LevelStaticData>(LevelStaticDataPath)
                .ToDictionary(staticData => staticData.LevelName, staticData => staticData);
            
            _entityStaticDataDictionary = Resources.LoadAll<EntityStaticData>(EntityStaticDataPath)
                .ToDictionary(staticData => staticData.EntityType, staticData => staticData);
        }

        public LevelStaticData GetLevelStaticData(LevelName levelName) => 
            _levelStaticDataDictionary[levelName];

        public EntityStaticData GetEntityStaticData(EntityType entityType) => 
            _entityStaticDataDictionary[entityType];
    }
}