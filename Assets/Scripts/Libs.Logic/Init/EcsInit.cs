using Leopotam.EcsLite;
using Logic.Ecs.Systems;
using UnityEngine;
using Zenject;

namespace Libs.Logic.Init
{
    public class EcsInit : MonoBehaviour
    {
        private readonly DiContainer _diContainer;
        private EcsSystems _systems;

        [Inject]
        private void Init(
            PlayerInitSystem playerInitSystem)
        {
            var world = new EcsWorld();
            _systems = new EcsSystems(world);
            _systems
                .Add(playerInitSystem).Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems?.GetWorld()?.Destroy();
            _systems = null;
        }
    }
}
