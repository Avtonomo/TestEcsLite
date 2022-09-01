using Leopotam.EcsLite;
using Logic.Ecs.Components.Lvl;
using Logic.Ecs.Components.Runtime;
using UnityEngine;

namespace Logic.Ecs.Systems
{
    public class UserMouseInputSystem : IEcsRunSystem
    {
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
            if (!Physics.Raycast(ray, out var hit)) return;
            var targetWorldPosition = hit.point;
            
            var world = systems.GetWorld();
            var filter = world.Filter<Hero>().End();
            var heroesInput = world.GetPool<MoveTarget>();

            foreach (var heroEntity in filter)
            {
                if (heroesInput.Has(heroEntity))
                {
                    ref var newInput = ref heroesInput.Get(heroEntity);
                    newInput.TargetPosition = new Vector3(targetWorldPosition.x, 0, targetWorldPosition.z);
                }
                else
                {
                    ref var newInput = ref heroesInput.Add(heroEntity);
                    newInput.TargetPosition = new Vector3(targetWorldPosition.x, 0, targetWorldPosition.z);;
                }
            }
        }
    }
}
