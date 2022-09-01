using Libs.Logic.SceneViews.Interfaces;
using UnityEngine;

namespace Libs.Logic.SceneViews
{
    public class HeroUnit : MonoBehaviour, IPositionSetter, IStateSetter, IRotationSetter, IAnimatorParametersSetter
    {
        private static readonly int WalkBoolId = Animator.StringToHash("Grounded");
        private static readonly int MovementSpeedId = Animator.StringToHash("MoveSpeed");

        [SerializeField] private Animator _animator;
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetDirection(Vector3 direction)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        public void SetWalkState(bool enable)
        {
            if (_animator.GetBool(WalkBoolId) == enable) return;
            
            _animator.SetBool(WalkBoolId, enable);
        }

        public void SetAnimatorMovementSpeed(float value)
        {
            _animator.SetFloat(MovementSpeedId, value);
        }
    }
}
