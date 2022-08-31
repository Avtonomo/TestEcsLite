using JetBrains.Annotations;
using Leopotam.EcsLite;
using Libs.Logic;
using Libs.Logic.Containers;
using Libs.Logic.Loaders;
using Libs.Logic.Providers;
using Libs.Logic.SceneViews;
using Logic.Ecs.Components.Lvl;
using UnityEngine;

namespace Logic.Ecs.Systems
{
    [UsedImplicitly]
    public class HeroInitSystem : IEcsInitSystem
    {
        private const string HeroPrefab = "Prefabs/Heroes/Hero";
        
        private readonly ResourceLoadService _resourceLoadService;
        private readonly SpawnPointsProvider _spawnPointsProvider;
        private readonly RunTimeObjectsContainer _runTimeObjectsContainer;
        private readonly LevelUnit _levelUnit;

        public HeroInitSystem(
            ResourceLoadService resourceLoadService,
            SpawnPointsProvider spawnPointsProvider,
            RunTimeObjectsContainer runTimeObjectsContainer,
            LevelUnit levelUnit)
        {
            _resourceLoadService = resourceLoadService;
            _spawnPointsProvider = spawnPointsProvider;
            _runTimeObjectsContainer = runTimeObjectsContainer;
            _levelUnit = levelUnit;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var playerEntity = world.NewEntity();
            var heroesPool = world.GetPool<Hero>();
            var currentPositionPool = world.GetPool<CurrentPositionComponent>();
            var synchronizePool = world.GetPool<PositionSynchronize>();
            ref var hero = ref heroesPool.Add(playerEntity);
            ref var currentPosition = ref currentPositionPool.Add(playerEntity);
            ref var sync = ref synchronizePool.Add(playerEntity);
            
            var heroGO = _resourceLoadService.Load(HeroPrefab);
            
            if (heroGO == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Missing hero prefab!");
#endif
            }

            var heroSpawnPosition = _spawnPointsProvider.GetHeroSpawnPoint();
            
            //TODO: выпилить зависимость на юнити
            var playerView = Object.Instantiate(heroGO, heroSpawnPosition, Quaternion.identity, _levelUnit.RunTimeUnitsContainer);
            
            _runTimeObjectsContainer.AddNew(playerView.GetInstanceID(), playerView);
            currentPosition.Position = heroSpawnPosition;
            hero.InstanceId = playerView.GetInstanceID();
            sync.InstanceId = playerView.GetInstanceID();
        }
    }
}