using UnityEngine;

namespace Assets.CodeBase.Services.InputService
{
    public class FakeInput : IInputService
    {
        public Vector2 MoveAxis => Vector2.zero;

        public Vector2 PointerAxis => Vector2.zero;

        public bool IsDashButtonDown => false;

        public Vector3 PointerPosition => Vector3.zero;
    }
}
