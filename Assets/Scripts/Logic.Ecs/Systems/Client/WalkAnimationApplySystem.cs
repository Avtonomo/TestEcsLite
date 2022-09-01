using Leopotam.EcsLite;
using Libs.Logic.Containers;
using Libs.Logic.SceneViews;
using Libs.Logic.SceneViews.Interfaces;
using Logic.Ecs.Components.Client;
using Logic.Ecs.Components.Server;
using UnityEngine;

namespace Logic.Ecs.Systems.Client
{
    public class WalkAnimationApplySystem : IEcsRunSystem
    {
        private readonly RunTimeObjectsContainer _runTimeObjectsContainer;

        public WalkAnimationApplySystem(RunTimeObjectsContainer runTimeObjectsContainer)
        {
            _runTimeObjectsContainer = runTimeObjectsContainer;
        }
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var inputs = world.GetPool<MoveTarget>();
            var walkAnimationPool = world.GetPool<HasWalkAnimation>();
            var instances = world.GetPool<InstanceId>();

            var unitsWithWalk = world.Filter<HasWalkAnimation>().End ();

            foreach (var walkEntity in unitsWithWalk)
            {
                if (!walkAnimationPool.Has(walkEntity)) continue;
                
                ref var instance = ref instances.Get (walkEntity);

                var go = _runTimeObjectsContainer.Get(instance.Id);
                var animationSetter = go?.GetComponent<IStateSetter>();
                if (animationSetter == null)
                {
#if UNITY_EDITOR
                    Debug.LogError("Invalid Unit. Missing IStateSetter");
#endif
                    return;
                }

                var isMove = inputs.Has(walkEntity);
                animationSetter.SetWalkState(isMove);
            }
        }
    }
}
