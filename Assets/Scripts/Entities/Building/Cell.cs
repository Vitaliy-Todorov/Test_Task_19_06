using Entities.Building.Components;
using UnityEngine;

namespace Entities.Building
{
    public class Cell : MonoBehaviour
    {
        [field: SerializeField] private Building _building;

        private void Awake()
        {
            if(!_building)
                return;
            _building.MoveBuilding.Placement(this);
            _building.OnDestroyed += RemoveBuilding;
        }

        public void PlaceBuilding(MoveBuilding moveBuilding)
        {
            if (_building == moveBuilding.Building) 
                return;
            if (_building)
                _building.Stack(moveBuilding.Building);
            else
                _building = moveBuilding.Building;
        }

        public void RemoveBuilding(Building building)
        {
            if(_building.ID != building.ID)
                return;
            _building.OnDestroyed -= RemoveBuilding;

            _building = null;
        }
    }
}