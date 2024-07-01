using System.Collections;
using Entities.Building.Components;
using Entities.HP;
using Infrastructure.DataServiceNamespace;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Entities.Buildings.Components
{
    public class ShootingBuilding : MonoBehaviour
    {
        [SerializeField] private float _speedShooting;
        [SerializeField] private Building.Building _building;
        [SerializeField] private MoveBuilding _moveBuilding;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _gun;

        private DataService _dataService;
        private EnemiesSpawner _enemiesSpawner;
        
        private Transform _currentTarget;

        [Inject] private void Construct(DataService dataService, EnemiesSpawner enemiesSpawner)
        {
            _dataService = dataService;
            _enemiesSpawner = enemiesSpawner;
        }

        private void Start() => 
            StartCoroutine(Shot());

        private void Update()
        {
            if(_currentTarget)
                transform.rotation = Quaternion.LookRotation(Vector3.forward, _currentTarget.position - transform.position);
        }

        private IEnumerator Shot()
        {
            while(true)
            {
                if (_moveBuilding.IsPlace && _enemiesSpawner.Enemies.Count > 0)
                {
                    GameObject bulletGO = Instantiate(_bullet, _gun.position, _gun.rotation);
                    bulletGO.GetComponent<Damage>().SetDamage(_building.Level * _dataService.DPSOneLevelBuilding() * _speedShooting);
                    _currentTarget = GetNearestTarget();
                    bulletGO.transform.rotation = Quaternion.LookRotation(Vector3.forward, _currentTarget.position - bulletGO.transform.position);
                }

                yield return new WaitForSeconds(_speedShooting);
            }
        }

        private Transform GetNearestTarget()
        {
            float shortestPath = float.MaxValue;
            float currentPath;
            Transform nearestPosition = transform;
            
            foreach (var (_, enemy) in _enemiesSpawner.Enemies)
            {
                currentPath = (enemy.transform.position - transform.position).magnitude;
                if (shortestPath > currentPath)
                {
                    shortestPath = currentPath;
                    nearestPosition = enemy.transform;
                }
            }

            return nearestPosition;
        }
    }
}