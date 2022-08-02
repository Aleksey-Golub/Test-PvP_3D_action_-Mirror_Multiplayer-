using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public class TransformRotator : RotatorBase
    {
        public override void RotateIn(Vector3 direction, float deltaTime)
        {
            if (direction == Vector3.zero)
                return;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, RotationSpeed * deltaTime * Mathf.Deg2Rad, 0.0f);
            newDirection.y = transform.position.y;
            transform.rotation = Quaternion.LookRotation(newDirection);

            Vector3 newLocalRot = transform.localEulerAngles;
            newLocalRot.x = 0;
            transform.localEulerAngles = newLocalRot;
        }
    }
}