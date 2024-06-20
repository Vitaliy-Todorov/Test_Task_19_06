using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Entities
{
    public class Placement : MonoBehaviour
    {
        private Building _building;

        private void Update()
        {
            if (!_building && Input.GetMouseButtonDown(0))
            {
                int layerMask = 1 << 6;
                // layerMask = ~layerMask;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    _building.OnPlaced += BuildingPlaced;
                    Debug.Log("Did hit");
                }
            }
        }

        private void BuildingPlaced()
        {
            _building.OnPlaced -= BuildingPlaced;
            _building = null;
        }
    }
}
