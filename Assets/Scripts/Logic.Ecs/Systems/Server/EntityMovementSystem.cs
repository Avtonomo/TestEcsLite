using Leopotam.EcsLite;
using Logic.Ecs.Components.Server;
using UnityEngine;

namespace Logic.Ecs.Systems.Server
{
    public class EntityMovementSystem : IEcsRunSystem
    {
        private const float MovementEps = 0.01f;
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var inputs = world.GetPool<MoveTarget>();
            var positions = world.GetPool<CurrentPosition>();
            var movementSpeeds = world.GetPool<MovementSpeed>();
            var filter = world.Filter<CurrentPosition>().End ();
            var deltaTime = Time.deltaTime;
            foreach (var entity in filter)
            {
                if (!inputs.Has(entity)) continue;

                ref var positionContainer = ref positions.Get (entity);
                ref var moveSpeed = ref movementSpeeds.Get (entity);

                var input = inputs.Get(entity);
                
                var dir = input.TargetPosition - positionContainer.Position;
                var newPosition = positionContainer.Position + dir.normalized * (moveSpeed.Value * deltaTime);
                
                positionContainer.Position = newPosition;
                positionContainer.Direction = dir.normalized;
                
                if (dir.sqrMagnitude < MovementEps)
                {
                    //Движение окончено
                    inputs.Del(entity);
                }
            }
        }
    }
}
