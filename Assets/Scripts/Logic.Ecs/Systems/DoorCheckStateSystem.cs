using Leopotam.EcsLite;
using Logic.Ecs.Components.Lvl;

namespace Logic.Ecs.Systems
{
    public class DoorCheckStateSystem : IEcsRunSystem
    {
        private const float DoorOpenEps = 0.01f;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var doorsPool = world.GetPool<Door>();
            var doors = world.Filter<Door>().End ();
            var positions = world.GetPool<CurrentPosition>();

            foreach (var doorEntity in doors)
            {
                ref var door = ref doorsPool.Get (doorEntity);
                if (door.IsOpen) continue;
                
                ref var doorPosition = ref positions.Get (doorEntity);
                var dir = door.OpenPosition - doorPosition.Position;

                if (dir.sqrMagnitude < DoorOpenEps)
                {
                    door.IsOpen = true;
                }
            }
        }
    }
}

