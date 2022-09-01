using UnityEngine;

namespace Logic.Ecs.Components.Server
{
    public struct Door
    {
        public bool IsOpen;
        
        public Vector3 ClosePosition;
        public Vector3 OpenPosition;
    }
}
