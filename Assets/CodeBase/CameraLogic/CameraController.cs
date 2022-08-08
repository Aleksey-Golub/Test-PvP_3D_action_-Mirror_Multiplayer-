using Assets.CodeBase.Services.InputService;
using UnityEngine;

namespace Assets.CodeBase.CameraLogic
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 100f;
        [SerializeField] private float _minXRotation = -30f;
        [SerializeField] private float _maxXRotation = 20f;
        [SerializeField] private bool _cameraInversion;

        private Transform _target;
        private IInputService _input;
        private float _localY;
        private float _localX;

        [field: SerializeField] public Camera Camera { get; private set; }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            Move();
            Rotate();
        }

        public void Construct(IInputService input)
        {
            _input = input;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Rotate()
        {
            Vector2 vector = _sensitivity * Time.deltaTime * _input.PointerAxis;
            int inversionFlag = _cameraInversion ? -1 : 1;


            _localY += -vector.y * inversionFlag;
            _localY = Mathf.Clamp(_localY, _minXRotation, _maxXRotation);
            _localX += vector.x * inversionFlag;

            transform.localEulerAngles = new Vector3(_localY, _localX, 0);
        }

        private void Move()
        {
            transform.position = _target.position;
        }
    }
}
