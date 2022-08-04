using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public abstract class MoverBase : MonoBehaviour
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 1f;
        
        public abstract float SpeedVectorMagnitude { get; }

        public void Construct(float movementSpeed)
        {
            MovementSpeed = movementSpeed;
        }

        /// <summary>
        /// moving at the speed specified in the component's inspector
        /// </summary>
        public abstract void Move(Vector3 direction, float deltaTime);

        /// <summary>
        /// moving at a specified speed
        /// </summary>
        public abstract void Move(Vector3 direction, float deltaTime, float speed);
    }
}