using JetBrains.Annotations;
using Leopotam.EcsLite;
using Libs.Logic.Containers;
using Libs.Logic.SceneViews;
using Logic.Ecs.Components.Server;
using UnityEngine;

namespace Logic.Ecs.Systems.Client
{
    [UsedImplicitly]
    public class ButtonsInitSystem : IEcsInitSystem
    {
        private readonly RunTimeObjectsContainer _runTimeObjectsContainer;
        private readonly LevelUnit _levelUnit;

        public ButtonsInitSystem(
            RunTimeObjectsContainer runTimeObjectsContainer,
            LevelUnit levelUnit)
        {
            _runTimeObjectsContainer = runTimeObjectsContainer;
            _levelUnit = levelUnit;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            foreach (var levelButtonUnit in _levelUnit.Buttons)
            {
                var buttonEntity = world.NewEntity();
                var buttonPool = world.GetPool<LevelButton>();
                var positionsPool = world.GetPool<CurrentPosition>();
                ref var levelButton = ref buttonPool.Add(buttonEntity);
                ref var buttonPosition = ref positionsPool.Add(buttonEntity);

                var buttonInstanceId = levelButtonUnit.GetInstanceID();
                var linkedDoor = levelButtonUnit.DoorUnit;

                if (linkedDoor == null)
                {
#if UNITY_EDITOR
                    Debug.LogError("Missing door for button");
                    return;
#endif
                }

                _runTimeObjectsContainer.AddNew(buttonInstanceId, levelButtonUnit.gameObject);

                levelButton.SelfInstanceId = buttonInstanceId;
                levelButton.LinkDoorInstanceId = linkedDoor.GetInstanceID();
                buttonPosition.Position = levelButtonUnit.transform.position;
            }
        }
    }
}