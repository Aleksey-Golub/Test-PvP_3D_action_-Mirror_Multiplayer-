using UnityEngine;

namespace Assets.CodeBase.Services.InputService
{
    public class FakeInput : IInputService
    {
        public Vector2 MoveAxis => Vector2.zero;
        public Vector2 PointerAxis => Vector2.zero;

        public Vector3 PointerPosition => Vector3.zero;
        public bool IsMainPointerButtonDown => false;
        public bool IsEscButtonDown => false;
    }
}
