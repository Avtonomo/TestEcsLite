using UnityEngine;

namespace Logic.Ecs.Components.Lvl
{
    public struct Door
    {
        public bool IsOpen;
        public int LinkButtonInstanceId;
        public int SelfInstanceId;
        
        public Vector3 ClosePosition;
        public Vector3 OpenPosition;
    }
}
