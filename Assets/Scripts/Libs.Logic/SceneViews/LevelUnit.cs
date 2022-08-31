using UnityEngine;

namespace Libs.Logic.SceneViews
{
    public class LevelUnit : MonoBehaviour
    {
        [SerializeField] private FloorGridUnit _floorGridUnit;
        [SerializeField] private Transform _runTimeUnitsContainer;

        public Transform RunTimeUnitsContainer => _runTimeUnitsContainer;
    }
}
