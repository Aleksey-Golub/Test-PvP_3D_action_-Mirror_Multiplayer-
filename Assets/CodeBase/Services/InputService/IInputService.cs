using UnityEngine;

namespace Assets.CodeBase.Services.InputService
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        Vector2 PointerAxis { get; }
        bool IsDashButtonDown { get; }
        Vector3 PointerPosition { get; }
    }
}
