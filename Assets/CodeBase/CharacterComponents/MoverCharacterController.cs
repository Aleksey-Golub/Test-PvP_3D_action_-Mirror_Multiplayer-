using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    [RequireComponent(typeof(CharacterController))]
    public class MoverCharacterController : MoverBase
    {
        [SerializeField] private CharacterController _characterController;

        public override float SpeedVectorMagnitude => _characterController.velocity.magnitude;

        public override void Move(Vector3 direction, float deltaTime)
        {
            Move(direction, deltaTime, MovementSpeed);
        }

        public override void Move(Vector3 direction, float deltaTime, float speed)
        {
            _characterController.Move(speed * deltaTime * direction);
        }
    }
}