using Assets.CodeBase.Services.Input;
using UnityEngine;

namespace Assets.CodeBase.CameraLogic
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _treshold = 30f;
        [Tooltip("Max X camera position, Min X = -MaxX")]
        [SerializeField] private float _maxX = 16.5f;
        [Tooltip("Max Z camera position, Min Z = -MaxZ")]
        [SerializeField] private float _maxZ = 16.5f;
        private IInputService _input;

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;

            Vector3 mouse = _input.PointerPosition;

            Vector2Int moveDirection = new Vector2Int();

            if (mouse.x < _treshold)
                moveDirection.x = -1;
            else if (mouse.x > Screen.width - _treshold)
                moveDirection.x = 1;

            if (mouse.y < _treshold)
                moveDirection.y = -1;
            else if (mouse.y > Screen.height - _treshold)
                moveDirection.y = 1;

            Move(moveDirection, deltaTime);
        }

        public void Construct(IInputService input)
        {
            _input = input;
        }

        private void Move(Vector2Int direction, float deltaTime)
        {
            Vector3 newPos = transform.position;
            float x = newPos.x + _speed * deltaTime * direction.x;
            float z = newPos.z + _speed * deltaTime * direction.y;

            newPos.x = Mathf.Clamp(x, _maxX * -1, _maxX);
            newPos.z = Mathf.Clamp(z, _maxZ * -1, _maxZ);

            transform.position = newPos;
        }
    }
}
