using Leopotam.EcsLite;
using Libs.Logic.Containers;
using Libs.Logic.SceneViews;
using Libs.Logic.SceneViews.Interfaces;
using Logic.Ecs.Components.Client;
using Logic.Ecs.Components.Server;

namespace Logic.Ecs.Systems.Client
{
    //Работает только на клиенте
    public class EntityPositionSynchronizeSystem : IEcsRunSystem
    {
        private readonly RunTimeObjectsContainer _runTimeObjectsContainer;

        public EntityPositionSynchronizeSystem(RunTimeObjectsContainer runTimeObjectsContainer)
        {
            _runTimeObjectsContainer = runTimeObjectsContainer;
        }

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var synchronize = world.GetPool<TransformSynchronize>();
            var heroesPositions = world.GetPool<CurrentPosition>();
            var instances = world.GetPool<InstanceId>();

            var filter = world.Filter<TransformSynchronize>().End ();

            foreach (var heroEntity in filter)
            {
                ref var synchronizeObject = ref synchronize.Get (heroEntity);
                ref var heroPosition = ref heroesPositions.Get (heroEntity);
                ref var instance = ref instances.Get (heroEntity);

                var unit = _runTimeObjectsContainer.Get(instance.Id);
                if (unit == null) continue;

                if (synchronizeObject.Position)
                    unit.GetComponent<IPositionSetter>()?.SetPosition(heroPosition.Position);
                
                if (synchronizeObject.Rotation)
                    unit.GetComponent<IRotationSetter>()?.SetDirection(heroPosition.Direction);
            }
        }
    }
}

