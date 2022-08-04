using UnityEngine;

namespace Assets.CodeBase.Services.InputService
{
    public class DesktopInputService : IInputService
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";
        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";

        public Vector2 MoveAxis => new Vector2(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));

        public bool IsDashButtonDown => Input.GetKeyDown(KeyCode.Mouse0);

        public Vector3 PointerPosition => Input.mousePosition;

        public Vector2 PointerAxis => new Vector2(Input.GetAxis(MOUSE_X), Input.GetAxis(MOUSE_Y));
    }
}
