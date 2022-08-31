using UnityEngine;

namespace Logic.Ecs.Components.Lvl
{
    public struct LevelButton
    {
        public bool IsActive;
        public int LinkDoorInstanceId;
        public int SelfInstanceId;
    }
}
