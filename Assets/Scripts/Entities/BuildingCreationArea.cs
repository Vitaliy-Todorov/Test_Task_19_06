using System.Collections.Generic;
using Entities.Building;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.EntityStaticData;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class BuildingCreationArea : MonoBehaviour
    {
        [SerializeField] private List<Cell> _placementCells;
        
        private BuildingsSpawner _buildingsSpawner;
        private StaticDataService _staticDataService;
        private EntityStaticData _buildingStaticData;

        [Inject]
        private void Construct(StaticDataService staticDataService, BuildingsSpawner buildingsSpawner)
        {
            _staticDataService = staticDataService;
            _buildingsSpawner = buildingsSpawner;

            _buildingStaticData = _staticDataService.GetEntityStaticData(EntityType.Building);
        }

        public bool AddBuilding()
        {
            foreach (Cell cell in _placementCells)
            {
                if (!cell.Building)
                {
                    Building.Building building = _buildingsSpawner.CreateBuild(_buildingStaticData.Prefab);
                    building.MoveBuilding.Placement(cell);
                    return true;
                }
            }
            return false;
        }
    }
}