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

            foreach (var levelButtonEntity in filterButtons)
            {
                ref var button = ref buttonsPool.Get (levelButtonEntity);
                
                // if (!button.IsActive) continue;

                var doorId = button.LinkDoorInstanceId;

                foreach (var doorEntity in doors)
                {
                    ref var door = ref doorsPool.Get (doorEntity);
                    
                    if (door.SelfInstanceId == doorId)
                    {
                        if (button.IsActive)
                        {
                            if (!moveTargetPool.Has(doorEntity))
                            {
                                ref var newInput = ref moveTargetPool.Add(doorEntity);
                                newInput.TargetPosition = door.OpenPosition;
                            }
                        }
                        else
                        {
                            if (moveTargetPool.Has(doorEntity))
                            {
                                moveTargetPool.Del(doorEntity);
                            }
                        }
                    }
                }
            }
        }
    }
}

