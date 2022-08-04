using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public class CharacterViewer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator _animator;
        [SerializeField] private SkinnedMeshRenderer _meshRenderer;
        [Header("Settings")]
        [SerializeField] private Material _hitedMaterial;
        
        private readonly int _speedHash = Animator.StringToHash("Speed");
        private readonly int _dashHash = Animator.StringToHash("Dash");
        
        private Material _defaultMaterial;

        private void Start()
        {
            _defaultMaterial = _meshRenderer.material;
        }

        public void PlayMove(float speedVectorMagnitude) => _animator.SetFloat(_speedHash, speedVectorMagnitude);

        public void PlayDash() => _animator.SetBool(_dashHash, true);

        public void StopDash() => _animator.SetBool(_dashHash, false);

        public void SetDamaged(bool state)
        {
            _meshRenderer.material = state ? _hitedMaterial : _defaultMaterial;
        }
    }
}