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

            foreach (var entity in filter)
            {
                if (!inputs.Has(entity)) continue;

                ref var positionContainer = ref positions.Get (entity);
                ref var moveSpeed = ref movementSpeeds.Get (entity);

                var input = inputs.Get(entity);
                
                var newPosition = Vector3.MoveTowards(positionContainer.Position, input.TargetPosition, Time.deltaTime * moveSpeed.Value);
                var dir = input.TargetPosition - positionContainer.Position;

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
