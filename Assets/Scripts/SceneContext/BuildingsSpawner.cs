using System.Collections.Generic;
using Entities;
using Entities.Building;
using UnityEngine;
using Zenject;

namespace SceneContext
{
    public class BuildingsSpawner
    {
        private IInstantiator _instantiator;

        public Dictionary<string, Building> Buildings { get; }

        private BuildingsSpawner(IInstantiator instantiator)
        {
            _instantiator = instantiator;

            Buildings = new Dictionary<string, Building>();
        }

        public Building CreateBuild(GameObject buildingPrefab)
        {
            GameObject buildingGO = _instantiator.InstantiatePrefab(buildingPrefab, Helper.WorldMousePosition(), Quaternion.identity);
            Building building = buildingGO.GetComponent<Building>();
            return building;
        }

        public void BuildingDestroy(string id)
        {
            GameObject gameObject = Buildings[id].gameObject;
            Debug.Log(gameObject);
            GameObject.Destroy(gameObject);
            Buildings.Remove(id);
        }

        public void BuildingWasCreated(string id, Building building) => 
            Buildings.Add(id, building);
    }
}