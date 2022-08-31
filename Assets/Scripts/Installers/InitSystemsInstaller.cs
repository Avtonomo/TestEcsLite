using Logic.Ecs.Systems;
using Zenject;

namespace Installers
{
    public class InitSystemsInstaller : Installer<InitSystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerInitSystem>().AsSingle();
        }
    }
}
