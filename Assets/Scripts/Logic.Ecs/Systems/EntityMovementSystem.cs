using Leopotam.EcsLite;
using Libs.Logic.Containers;
using Logic.Ecs.Components.Lvl;
using Logic.Ecs.Components.Runtime;
using UnityEngine;

namespace Logic.Ecs.Systems
{
    public class EntityMovementSystem : IEcsRunSystem
    {
        private const float MovementEps = 0.001f;
        
        private readonly RunTimeObjectsContainer _runTimeObjectsContainer;

        public EntityMovementSystem(RunTimeObjectsContainer runTimeObjectsContainer)
        {
            _runTimeObjectsContainer = runTimeObjectsContainer;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var inputs = world.GetPool<MoveTarget>();
            var heroes = world.GetPool<CurrentPositionComponent>();
            var filter = world.Filter<CurrentPositionComponent>().End ();

            foreach (var heroEntity in filter)
            {
                if (!inputs.Has(heroEntity)) continue;

                ref var hero = ref heroes.Get (heroEntity);

                var input = inputs.Get(heroEntity);
                
                var newPosition = Vector3.MoveTowards(hero.Position, input.TargetPosition, Time.deltaTime);
                hero.Position = newPosition;
                
                var dir = input.TargetPosition - hero.Position;
                if (dir.sqrMagnitude < MovementEps)
                {
                    //Движение окончено
                    inputs.Del(heroEntity);
                }
            }
        }
    }
}
