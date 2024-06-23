using System;
using System.Globalization;
using Entities;
using Entities.Building;
using Infrastructure.DataServiceNamespace;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.WindowsManager;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class SceneMonoInstaller : MonoInstaller
    {
        [SerializeField] private BuildingCreationArea _buildingCreationArea;
        
        public override void InstallBindings()
        {
            Container
                .Bind<BuildingCreationArea>()
                .FromInstance(_buildingCreationArea)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<StaticDataService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<DataService>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<TimeController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<GameWindowsManager>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<WaveController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<Counter>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<BuildingsSpawner>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<EnemiesSpawner>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            Container
                .BindInterfacesAndSelfTo<Placement>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}
