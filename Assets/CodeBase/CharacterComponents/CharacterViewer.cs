using UnityEngine;

namespace Assets.CodeBase.CharacterComponents
{
    public class CharacterViewer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _speedHash = Animator.StringToHash("Speed");
        private readonly int _dashHash = Animator.StringToHash("Dash");

        public void PlayMove(float speedVectorMagnitude) => _animator.SetFloat(_speedHash, speedVectorMagnitude);

        public void PlayDash() => _animator.SetBool(_dashHash, true);

        public void StopDash() => _animator.SetBool(_dashHash, false);
    }
}