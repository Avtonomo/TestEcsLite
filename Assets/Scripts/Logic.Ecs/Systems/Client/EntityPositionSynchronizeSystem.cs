using Leopotam.EcsLite;
using Libs.Logic.Containers;
using Libs.Logic.SceneViews;
using Logic.Ecs.Components.Lvl;

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
            var heroes = world.GetPool<PositionSynchronize>();
            var heroesPositions = world.GetPool<CurrentPositionComponent>();
            var filter = world.Filter<PositionSynchronize>().End ();

            foreach (var heroEntity in filter)
            {
                ref var hero = ref heroes.Get (heroEntity);
                ref var heroPosition = ref heroesPositions.Get (heroEntity);

                var unit = _runTimeObjectsContainer.Get(hero.InstanceId);
                if (unit == null) continue;
                var positionSetter = unit.GetComponent<IPositionSetter>();

                positionSetter.SetPosition(heroPosition.Position);
            }
        }
    }
}

