using System.Collections.Generic;
using UnityEngine;

namespace Libs.Logic.SceneViews
{
    public class LevelUnit : MonoBehaviour
    {
        [SerializeField] private FloorGridUnit _floorGridUnit;
        [SerializeField] private Transform _runTimeUnitsContainer;
       
        [SerializeField] private DoorUnit[] _doors;
        [SerializeField] private ButtonUnit[] _buttons;

        public Transform RunTimeUnitsContainer => _runTimeUnitsContainer;
        public IReadOnlyCollection<DoorUnit> Doors => _doors;
        public IReadOnlyCollection<ButtonUnit> Buttons => _buttons;
    }
}
