using System;
using UnityEngine;

namespace Entities
{
    public class Building : MonoBehaviour
    {
        public event Action OnPlaced;
        [field: SerializeField] public bool IsPlace { get; private set; }

        private void Update()
        {
            if(IsPlace)
                return;
            
            transform.position = Helper.WorldMousePosition();
        }

        public void IsMoving()
        {
            IsPlace = false;
        }

        public bool Placement()
        {
            IsPlace = true;
            OnPlaced?.Invoke();

            return true;
        }
    }
}