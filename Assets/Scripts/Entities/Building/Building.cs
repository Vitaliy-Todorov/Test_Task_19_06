using System;
using Entities.Building.Components;
using SceneContext;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Entities.Building
{
    public class Building : MonoBehaviour
    {
        public string ID { get; private set; }
        [field: SerializeField] public int Level { get; private set; } = 1;
        [field: SerializeField] public MoveBuilding MoveBuilding { get; private set; }
        
        private BuildingsSpawner _buildingsSpawner;

        private void Awake()
        {
            ID = Guid.NewGuid().ToString();
            _buildingsSpawner.BuildingWasCreated(ID, transform);
        }

        [Inject]
        public virtual void Construct(BuildingsSpawner buildingsSpawner) => 
            _buildingsSpawner = buildingsSpawner;

        private void OnDestroy() => 
            _buildingsSpawner.BuildingWasDestroyed(ID);

        public void Stack(Building building)
        {
            Level += building.Level;
            Destroy(building.gameObject);
        }
    }
}