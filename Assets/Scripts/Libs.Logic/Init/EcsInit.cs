using Leopotam.EcsLite;
using Logic.Ecs.Systems.Client;
using Logic.Ecs.Systems.Server;
using UnityEngine;
using Zenject;

namespace Libs.Logic.Init
{
    public class EcsInit : MonoBehaviour
    {
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
            DoorOpenSystem doorOpenSystem,
            WalkAnimationApplySystem walkAnimationApplySystem,
            DoorCheckStateSystem doorCheckStateSystem)
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
                .Add(entityPositionSynchronizeSystem)
                .Add(walkAnimationApplySystem)
                .Add(doorCheckStateSystem)
                .Add(entityMovementSystem)
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
