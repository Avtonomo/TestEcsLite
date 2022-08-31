using UnityEngine;

namespace Libs.Logic.SceneViews
{
    public class HeroUnit : MonoBehaviour, IPositionSetter
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
