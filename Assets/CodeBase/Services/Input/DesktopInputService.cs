using UnityEngine;

namespace Assets.CodeBase.Services.Input
{
    public class DesktopInputService : IInputService
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        public Vector2 Axis => new Vector2(UnityEngine.Input.GetAxis(HORIZONTAL), UnityEngine.Input.GetAxis(VERTICAL));

        public bool IsDashButtonDown => UnityEngine.Input.GetKeyDown(KeyCode.Mouse0);

        public Vector3 PointerPosition => UnityEngine.Input.mousePosition;
    }
}
