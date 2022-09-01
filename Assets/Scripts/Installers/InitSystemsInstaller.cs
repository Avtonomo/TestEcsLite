using Logic.Ecs.Systems;
using Logic.Ecs.Systems.Client;
using Zenject;

namespace Installers
{
    public class InitSystemsInstaller : Installer<InitSystemsInstaller>
    {
        public override void InstallBindings()
        {
            //Init systems
            Container.Bind<HeroInitSystem>().AsSingle();
            Container.Bind<ButtonsInitSystem>().AsSingle();
            Container.Bind<DoorsInitSystem>().AsSingle();
            
            //Run systems
            Container.Bind<UserMouseInputSystem>().AsSingle();
            Container.Bind<EntityMovementSystem>().AsSingle();
            Container.Bind<EntityPositionSynchronizeSystem>().AsSingle();
            Container.Bind<ButtonCollisionSystem>().AsSingle();
            Container.Bind<DoorOpenSystem>().AsSingle();
            Container.Bind<WalkAnimationApplySystem>().AsSingle();
            Container.Bind<DoorCheckStateSystem>().AsSingle();
        }
    }
}
