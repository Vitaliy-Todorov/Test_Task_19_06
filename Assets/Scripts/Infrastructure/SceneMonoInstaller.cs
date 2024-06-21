using Entities.Building;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using SceneContext;
using Zenject;

namespace Infrastructure
{
    public class SceneMonoInstaller : MonoInstaller
    {
        // [SerializeField] private NavMeshSurface _surface;
        
        public override void InstallBindings()
        {
            /*Container
                .Bind<NavMeshSurface>()
                .FromInstance(_surface)
                .AsSingle();*/
            
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
                .BindInterfacesAndSelfTo<WaveController>()
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
