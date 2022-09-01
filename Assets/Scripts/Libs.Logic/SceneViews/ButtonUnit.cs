using UnityEngine;

namespace Libs.Logic.SceneViews
{
    public class ButtonUnit : MonoBehaviour
    {
        [SerializeField] private DoorUnit _doorUnit;

        public DoorUnit DoorUnit => _doorUnit;
    }
}
