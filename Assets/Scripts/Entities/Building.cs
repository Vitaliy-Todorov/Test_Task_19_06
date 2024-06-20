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

            if (Input.GetMouseButtonDown(0)) 
                Placement();
        }

        public void Placement()
        {
            IsPlace = true;
            OnPlaced?.Invoke();
        }
    }
}