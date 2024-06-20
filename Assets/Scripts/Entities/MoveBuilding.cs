using System;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Building))]
    public class MoveBuilding : MonoBehaviour
    {
        public event Action<MoveBuilding> OnPlaced;

        [field: SerializeField] public Building Building { get; private set; }
        [field: SerializeField] public bool IsPlace { get; private set; }
        [field: SerializeField] public Cell SelectedCell { get; private set; }
        [field: SerializeField] private Cell _lastCell;

        private void Awake()
        {
            Building = GetComponent<Building>();
        }

        private void Update()
        {
            if(IsPlace)
                return;
            
            transform.position = Helper.WorldMousePosition();
        }

        private void OnCollisionEnter2D(Collision2D col) => 
            SelectedCell = col.gameObject.GetComponent<Cell>();

        private void OnCollisionExit2D(Collision2D other) => 
            SelectedCell = null;

        public void IsMoving() => 
            IsPlace = false;

        public bool Placement()
        {
            if(SelectedCell) 
                _lastCell = SelectedCell;
            transform.position = _lastCell.transform.position;
            IsPlace = true;
            OnPlaced?.Invoke(this);

            return true;
        }
        public void Placement(Cell cell)
        {
            _lastCell = cell;
            Placement();
        }
    }
}