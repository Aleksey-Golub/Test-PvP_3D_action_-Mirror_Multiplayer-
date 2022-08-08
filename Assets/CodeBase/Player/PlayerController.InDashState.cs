using System;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private class InDashState : PlayerStateBase
        {
            private bool _dashEneded;

            public InDashState(PlayerController controller) : base(controller)
            {
            }

            public override void Enter()
            {
                Controller._viewer.PlayDash();
            }

            public override void Execute(float deltaTime)
            {
                Vector3 movementVector = Controller.transform.forward;

                Controller._rotator.RotateIn(movementVector.normalized, deltaTime);
                _dashEneded = Controller._dasher.Dash(movementVector, deltaTime);

                CheckNeedAndDoTransitions();
            }

            public override void Exit()
            {
                Controller._viewer.StopDash();
            }

            protected override bool CheckNeedAndDoTransitions()
            {
                if (_dashEneded)
                {
                    Controller.TransitionTo<SimpleState>();
                    return true;
                }

                return false;
            }
        }

        internal void SetScoreText(object value)
        {
            throw new NotImplementedException();
        }
    }
}