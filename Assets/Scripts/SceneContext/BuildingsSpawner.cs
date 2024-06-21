using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SceneContext
{
    public class BuildingsSpawner
    {
        private IInstantiator _instantiator;

        public Dictionary<string, Transform> Buildings { get; }

        private BuildingsSpawner(IInstantiator instantiator)
        {
            _instantiator = instantiator;

            Buildings = new Dictionary<string, Transform>();
        }

        public void CreateBuild(GameObject buildingPrefab) => 
            _instantiator.InstantiatePrefab(buildingPrefab, Helper.WorldMousePosition(), Quaternion.identity);

        public void BuildingWasDestroyed(string id) => 
            Buildings.Remove(id);

        public void BuildingWasCreated(string id, Transform building) => 
            Buildings.Add(id, building);
    }
}