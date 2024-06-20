using UnityEngine;

namespace Entities
{
    public class MoveBuilding : MonoBehaviour
    {
        [field: SerializeField] public bool IsPlace { get; private set; }
        [field: SerializeField] private Cell _selectedCell;
        [field: SerializeField] private Cell _lastCell;

        private void Awake() => 
            transform.position = _lastCell.transform.position;

        private void Update()
        {
            if(IsPlace)
                return;
            
            transform.position = Helper.WorldMousePosition();
        }

        private void OnCollisionEnter2D(Collision2D col) => 
            _selectedCell = col.gameObject.GetComponent<Cell>();

        private void OnCollisionExit2D(Collision2D other) => 
            _selectedCell = null;

        public void IsMoving() => 
            IsPlace = false;

        public bool Placement()
        {
            if(_selectedCell) 
                _lastCell = _selectedCell;
            transform.position = _lastCell.transform.position;
            IsPlace = true;

            return true;
        }
    }
}