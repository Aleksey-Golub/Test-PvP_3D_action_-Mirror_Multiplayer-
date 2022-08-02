using UnityEngine;

namespace Assets.CodeBase.Logic.CharacterComponents
{
    public class Dasher : MonoBehaviour
    {
        [SerializeField] private float _rechargeTime = 5f;
        [SerializeField] private float _distance = 5f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private MoverBase _mover;

        private Timer _dashRechargeTimer = new Timer();
        private Timer _dashMakeTimer = new Timer();
        private bool _inDash;

        public bool CanDash => _dashRechargeTimer.Value >= _rechargeTime;
        public float DashSpeed => _distance / _duration;

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

            if (_dashMakeTimer.Value >= _duration)
            {
                _dashMakeTimer.Reset();
                _inDash = false;
                return true;
            }
            return false;
        }
    }
}