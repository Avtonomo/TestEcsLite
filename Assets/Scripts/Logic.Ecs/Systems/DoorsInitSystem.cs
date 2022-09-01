using System.Linq;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Libs.Logic.Containers;
using Libs.Logic.SceneViews;
using Logic.Ecs.Components.Lvl;
using UnityEngine;

namespace Logic.Ecs.Systems
{
    [UsedImplicitly]
    public class DoorsInitSystem : IEcsInitSystem
    {
        private const float DoorOpenSpeed = 0.35f;

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
            var doorsPool = world.GetPool<Door>();
            var syncPool = world.GetPool<TransformSynchronize>();
            var positionPool = world.GetPool<CurrentPosition>();
            var instances = world.GetPool<InstanceId>();
            var movementSpeeds = world.GetPool<MovementSpeed>();

            foreach (var doorUnit in _levelUnit.Doors)
            {
                var doorEntity = world.NewEntity();
               
                ref var currentDoor = ref doorsPool.Add(doorEntity);
                ref var doorPos = ref positionPool.Add(doorEntity);
                ref var sync = ref syncPool.Add(doorEntity);
                ref var instance = ref instances.Add(doorEntity);
                ref var movementSpeed = ref movementSpeeds.Add(doorEntity);

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

                instance.Id = doorInstanceId;
                currentDoor.IsOpen = doorUnit.IsOpen;
                currentDoor.OpenPosition = doorUnit.OpenPosition;
                currentDoor.ClosePosition = doorUnit.ClosePosition;
                doorPos.Position = doorUnit.IsOpen ? doorUnit.OpenPosition : doorUnit.ClosePosition;
                sync.Position = true;
                movementSpeed.Value = DoorOpenSpeed;
            }
        }
    }
}