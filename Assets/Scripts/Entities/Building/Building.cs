using System;
using Entities.Building.Components;
using SceneContext;
using TMPro;
using UnityEngine;
using Zenject;

namespace Entities.Building
{
    public class Building : AEntity
    {
        public event Action<Building> OnDestroyed;

        [field: SerializeField] public int Level { get; private set; } = 1;
        [field: SerializeField] public MoveBuilding MoveBuilding { get; private set; }

        [SerializeField] private TMP_Text _levelText;

        private BuildingsSpawner _buildingsSpawner;

        [Inject]
        public virtual void Construct(BuildingsSpawner buildingsSpawner) => 
            _buildingsSpawner = buildingsSpawner;

        private void Awake()
        {
            ID = Guid.NewGuid().ToString();
            _buildingsSpawner.BuildingWasCreated(ID, this);
            
            _levelText.text = Level.ToString();
        }

        public override void DestroyEntity()
        {
            OnDestroyed?.Invoke(this);
            _buildingsSpawner.BuildingDestroy(ID);
        }

        public void Stack(Building building)
        {
            Level += building.Level;
            _buildingsSpawner.BuildingDestroy(building.ID);

            _levelText.text = Level.ToString();
        }
    }
}