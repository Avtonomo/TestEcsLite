using Libs.Logic;
using Libs.Logic.Containers;
using Libs.Logic.Loaders;
using Libs.Logic.Providers;
using Libs.Logic.SceneViews;
using Studio3.Toolkit;
using Zenject;

namespace Installers
{
    public class MainZenjectInstaller : MonoInstaller<MainZenjectInstaller>
    {
        private const string LevelGOName = "Level";
        
        public override void InstallBindings()
        {
            var levelUnit = ZenjectBindHelper.GetGameObjectOnSceneByName<LevelUnit>(LevelGOName);

            Container.Bind<LevelUnit>().FromInstance(levelUnit);

            Container.Bind<ResourceLoadService>().AsSingle();
            Container.Bind<SpawnPointsProvider>().AsSingle();
            Container.Bind<RunTimeObjectsContainer>().AsSingle();
            
            InitSystemsInstaller.Install(Container);
        }
    }
}
