namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private class SimpleState : PlayerStateBase
        {
            public SimpleState(PlayerController controller) : base(controller)
            {
            }

            public override void Enter()
            {
            }

            public override void Execute(float deltaTime)
            {
                Controller._rotator.RotateIn(Controller._normalizedMovementVector, deltaTime);
                Controller._mover.Move(Controller._normalizedMovementVector, deltaTime);
                Controller._viewer.PlayMove(Controller._mover.SpeedVectorMagnitude);

                CheckNeedAndDoTransitions();
            }

            public override void Exit()
            {
            }

            protected override bool CheckNeedAndDoTransitions()
            {
                if (Controller._input.IsMainPointerButtonDown && Controller._dasher.CanDash)
                {
                    Controller.TransitionTo<InDashState>();
                    return true;
                }

                return false;
            }
        }
    }
}