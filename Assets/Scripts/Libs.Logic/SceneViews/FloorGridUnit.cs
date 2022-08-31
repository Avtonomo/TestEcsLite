using UnityEngine;

namespace Libs.Logic.SceneViews
{
    public class FloorGridUnit : MonoBehaviour
    {
        public CellUnit[] Cells;

        private void Awake()
        {
            Cells = GetComponentsInChildren<CellUnit>();
        }
    }
}
