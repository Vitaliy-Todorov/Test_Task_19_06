using Entities;
using Entities.Building;
using Zenject;

namespace Infrastructure
{
    public class SceneMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Placement>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}
