using Leopotam.EcsLite;
using Logic.Ecs.Components.Server;

namespace Logic.Ecs.Systems.Server
{
    public class ButtonCollisionSystem : IEcsRunSystem
    {
        private const float ButtonRadios = 0.2f;

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var buttons = world.GetPool<LevelButton>();
            var positions = world.GetPool<CurrentPosition>();
            var filterHeroes = world.Filter<Hero>().Inc<CurrentPosition>().End ();
            var filterButtons = world.Filter<LevelButton>().End ();

            foreach (var heroEntity in filterHeroes)
            {
                ref var hero = ref positions.Get(heroEntity);

                foreach (var levelButtonEntity in filterButtons)
                {
                    ref var buttonPosition = ref positions.Get(levelButtonEntity);
                    ref var button = ref buttons.Get(levelButtonEntity);
                    
                    var dir = hero.Position - buttonPosition.Position;
                    button.IsActive = dir.sqrMagnitude < ButtonRadios * ButtonRadios;
                }
            }
        }
    }
}
