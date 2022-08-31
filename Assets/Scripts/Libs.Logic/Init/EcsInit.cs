using Leopotam.EcsLite;
using Logic.Ecs.Systems;
using Logic.Ecs.Systems.Client;
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
            HeroInitSystem heroInitSystem,
            DoorsInitSystem doorsInitSystem,
            ButtonsInitSystem buttonsInitSystem,
            UserMouseInputSystem userMouseInputSystem,
            EntityMovementSystem entityMovementSystem,
            EntityPositionSynchronizeSystem entityPositionSynchronizeSystem,
            ButtonCollisionSystem buttonCollisionSystem,
            DoorOpenSystem doorOpenSystem)
        {
            var world = new EcsWorld();
            _systems = new EcsSystems(world);
            _systems
                .Add(heroInitSystem)
                .Add(doorsInitSystem)
                .Add(buttonsInitSystem)
                .Add(userMouseInputSystem)
                .Add(buttonCollisionSystem)
                .Add(doorOpenSystem)
                .Add(entityMovementSystem)
                .Add(entityPositionSynchronizeSystem)
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Init();
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
