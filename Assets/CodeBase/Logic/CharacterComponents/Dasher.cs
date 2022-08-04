using UnityEngine;
using System;
using Assets.CodeBase.Player;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public class Dasher : MonoBehaviour
    {
        [SerializeField] private PlayerController _controller;
        [SerializeField] private float _rechargeTime = 5f;
        [SerializeField] private float _distance = 5f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private MoverBase _mover;
        [Tooltip("Vetical capsule only. Collider parameters to register a hit. " +
            "Set equal to the character's collider parameters, but with an increased radius")]
        [SerializeField] private ColliderParams _colliderParams;
        [SerializeField] private LayerMask _layerMask;

        private Timer _dashRechargeTimer = new Timer();
        private Timer _dashMakeTimer = new Timer();
        private bool _inDash;
        private readonly Collider[] _hits = new Collider[10];

        public bool CanDash => _dashRechargeTimer.Value >= _rechargeTime;
        public float DashSpeed => _distance / _duration;

        private void OnDrawGizmosSelected()
        {
            DrawColliderToHit();
        }

        public void Tik(float deltaTime)
        {
            _dashRechargeTimer.Tik(deltaTime);
        }

        public bool Dash(Vector3 movementVector, float deltaTime)
        {
            if (_inDash == false)
            {
                _inDash = true;
                _dashRechargeTimer.Reset();
            }

            _dashMakeTimer.Tik(deltaTime);
            _mover.Move(movementVector, deltaTime, DashSpeed);

            CheckHitAndDoDamage();

            if (_dashMakeTimer.Value >= _duration)
            {
                _dashMakeTimer.Reset();
                _inDash = false;
                return true;
            }
            return false;
        }

        private void CheckHitAndDoDamage()
        {
            GetCapsuleFocuses(out Vector3 point0, out Vector3 point1);

            int hitCount = Physics.OverlapCapsuleNonAlloc(
                            point0,
                            point1,
                            _colliderParams.Radius,
                            _hits,
                            _layerMask);

            for (int i = 0; i < hitCount; i++)
            {
                if (_hits[i].TryGetComponent(out PlayerController controller) && controller != _controller && controller.IsImmortal == false)
                    controller.TakeDamage();
            }
        }

        private void DrawColliderToHit()
        {
            GetCapsuleFocuses(out Vector3 point0, out Vector3 point1);

            Gizmos.DrawWireSphere(point0, _colliderParams.Radius);
            Gizmos.DrawWireSphere(point1, _colliderParams.Radius);
            Gizmos.DrawWireSphere(_colliderParams.Center, _colliderParams.Radius);
        }

        private void GetCapsuleFocuses(out Vector3 point0, out Vector3 point1)
        {
            // distance from collider center to each sphere center
            float h = (_colliderParams.Height - _colliderParams.Radius * 2) * 0.5f;

            point0 = transform.position + _colliderParams.Center + Vector3.up * h;
            point1 = transform.position + _colliderParams.Center - Vector3.up * h;
        }
    }

    [Serializable]
    public struct ColliderParams
    {
        public float Radius;
        public float Height;
        public Vector3 Center;
    }
}