using UnityEngine;

namespace Libs.Logic.SceneViews
{
    public class DoorUnit : MonoBehaviour
    {
        [SerializeField] private Vector3 _closePosition;
        [SerializeField] private Vector3 _openPosition;
        
        public bool IsOpen { get; }
    }
}
