namespace Assets.CodeBase.Player
{
    public partial class PlayerController
    {
        private abstract class PlayerStateBase
        {
            protected readonly PlayerController Controller;

            protected PlayerStateBase(PlayerController controller)
            {
                Controller = controller;
            }

            public abstract void Enter();
            public abstract void Execute(float deltaTime);
            public abstract void Exit();
            protected abstract bool CheckNeedAndDoTransitions();
        }
    }
}