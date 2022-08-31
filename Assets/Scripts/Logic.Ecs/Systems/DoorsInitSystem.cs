using System.Linq;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Libs.Logic.Containers;
using Libs.Logic.SceneViews;
using Logic.Ecs.Components.Hero;
using UnityEngine;

namespace Logic.Ecs.Systems
{
    [UsedImplicitly]
    public class DoorsInitSystem : IEcsInitSystem
    {
        private readonly RunTimeObjectsContainer _runTimeObjectsContainer;
        private readonly LevelUnit _levelUnit;

        public DoorsInitSystem(
            RunTimeObjectsContainer runTimeObjectsContainer,
            LevelUnit levelUnit)
        {
            _runTimeObjectsContainer = runTimeObjectsContainer;
            _levelUnit = levelUnit;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            foreach (var doorUnit in _levelUnit.Doors)
            {
                var doorEntity = world.NewEntity();
                var doorsPool = world.GetPool<Door>();
                ref var currentDoor = ref doorsPool.Add(doorEntity);

                var doorInstanceId = doorUnit.GetInstanceID();

                var linkedButton = _levelUnit.Buttons.FirstOrDefault(buttonUnit =>
                    buttonUnit.DoorUnit.GetInstanceID() == doorInstanceId);
                if (linkedButton == null)
                {
#if UNITY_EDITOR
                Debug.LogError("Missing button for door");
                return;
#endif
                }
                
                _runTimeObjectsContainer.AddNew(doorInstanceId, doorUnit.gameObject);

                currentDoor.SelfInstanceId = doorInstanceId;
                currentDoor.LinkButtonInstanceId = linkedButton.GetInstanceID();
            }
        }
    }
}