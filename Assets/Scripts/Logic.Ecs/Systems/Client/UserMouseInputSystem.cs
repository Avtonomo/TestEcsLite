using Leopotam.EcsLite;
using Logic.Ecs.Components.Server;
using UnityEngine;

namespace Logic.Ecs.Systems.Client
{
    public class UserMouseInputSystem : IEcsRunSystem
    {
        private const string FloorLayerName = "Floor";
        private const float RayCastDistance = 100f;
        
        private readonly Camera _mainCamera;
        
        public UserMouseInputSystem()
        {
            _mainCamera = Camera.main;
        }

        public void Run(IEcsSystems systems)
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var targetPosition = Input.mousePosition;
            var ray = _mainCamera.ScreenPointToRay(targetPosition);
            var layerMask = LayerMask.GetMask(FloorLayerName);
            
            if (!Physics.Raycast(ray, out var hit, RayCastDistance, layerMask)) return;
            var targetWorldPosition = hit.point;
            
            var world = systems.GetWorld();
            var filter = world.Filter<Hero>().End();
            var heroesInput = world.GetPool<MoveTarget>();

            foreach (var heroEntity in filter)
            {
                var targetMovePosition = new Vector3(targetWorldPosition.x, 0, targetWorldPosition.z);
                
                if (heroesInput.Has(heroEntity))
                {
                    ref var newInput = ref heroesInput.Get(heroEntity);
                    newInput.TargetPosition = targetMovePosition;
                }
                else
                {
                    ref var newInput = ref heroesInput.Add(heroEntity);
                    newInput.TargetPosition = targetMovePosition;
                }
            }
        }
    }
}
