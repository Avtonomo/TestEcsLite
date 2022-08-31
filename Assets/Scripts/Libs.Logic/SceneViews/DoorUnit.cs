using UnityEngine;

namespace Libs.Logic.SceneViews
{
    public class DoorUnit : MonoBehaviour, IPositionSetter
    {
        [SerializeField] private Vector3 _closePosition;
        [SerializeField] private Vector3 _openPosition;
        [SerializeField] private Transform _doorPlate;
        
        public bool IsOpen { get; set; }
        
        public Vector3 ClosePosition => _closePosition;
        public Vector3 OpenPosition => _openPosition;
        
        public void SetPosition(Vector3 position)
        {
            _doorPlate.localPosition = position;
        }
    }
}
