using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class Cell : MonoBehaviour
    {
        [field: SerializeField] private Building _building;
        private Building _selectedBuilding;

        private void Awake()
        {
            if(!_building)
                return;
            _building.MoveBuilding.Placement(this);
            _building.MoveBuilding.OnPlaced += PlaceBuilding;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _selectedBuilding = col.gameObject.GetComponent<Building>();
            _building.MoveBuilding.OnPlaced -= PlaceBuilding;
            _selectedBuilding.MoveBuilding.OnPlaced += PlaceBuilding;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if(!_selectedBuilding)
                return;
            _selectedBuilding.MoveBuilding.OnPlaced -= PlaceBuilding;
            _building.MoveBuilding.OnPlaced += PlaceBuilding;
            _selectedBuilding = null;
        }

        private void PlaceBuilding(MoveBuilding moveBuilding)
        {
            if (moveBuilding.SelectedCell != this)
                if (_building != moveBuilding.Building)
                    _building.Stack(moveBuilding.Building);
        }
    }
}