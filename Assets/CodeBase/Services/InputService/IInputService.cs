using UnityEngine;

namespace Assets.CodeBase.Services.InputService
{
    public interface IInputService : IService
    {
        Vector2 MoveAxis { get; }
        Vector2 PointerAxis { get; }
        bool IsMainPointerButtonDown { get; }
        bool IsEscButtonDown { get; }
        Vector3 PointerPosition { get; }
    }
}
