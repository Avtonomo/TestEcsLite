using Leopotam.EcsLite;
using Logic.Ecs.Components.Lvl;
using Logic.Ecs.Components.Runtime;

namespace Logic.Ecs.Systems
{
    public class DoorOpenSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var buttonsPool = world.GetPool<LevelButton>();
            var doorsPool = world.GetPool<Door>();
            var filterButtons = world.Filter<LevelButton> ().End ();
            var doors = world.Filter<Door>().End ();
            var moveTargetPool = world.GetPool<MoveTarget>();
            var instances = world.GetPool<InstanceId>();

            foreach (var levelButtonEntity in filterButtons)
            {
                ref var button = ref buttonsPool.Get (levelButtonEntity);
                
                var doorId = button.LinkDoorInstanceId;

                foreach (var doorEntity in doors)
                {
                    ref var door = ref doorsPool.Get (doorEntity);
                    ref var instance = ref instances.Get (doorEntity);

                    if (instance.Id != doorId) continue;
                    
                    if (button.IsActive)
                    {
                        if (moveTargetPool.Has(doorEntity)) return;
                        
                        ref var newInput = ref moveTargetPool.Add(doorEntity);
                        newInput.TargetPosition = door.OpenPosition;
                        return;
                    }
                    
                    if (moveTargetPool.Has(doorEntity))
                    {
                        moveTargetPool.Del(doorEntity);
                    }
                }
            }
        }
    }
}

