using System;
using System.Globalization;
using Entities.Building;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.WindowsManager;
using SceneContext;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public static class Constants
    {
        public const string FirstLaunchDateKey = "FirstLaunchDate";
    }

    public class SceneMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<StaticDataService>()
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
