using Assets.CodeBase.CharacterComponents;
using Assets.CodeBase.Logic.CharacterComponents;
using Assets.CodeBase.Services.Input;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MoverBase _mover;
        [SerializeField] private RotatorBase _rotator;
        [SerializeField] private CharacterViewer _viewer;
        [SerializeField] private Dasher _dasher;

        [Header("Settings")]

        private IInputService _input;
        private PlayerStateBase _state;
        private Dictionary<Type, PlayerStateBase> _states;

        public bool IsImmortal { get; private set; }
        public RotatorBase Rotator { get => _rotator; set => _rotator = value; }

        public void Update()
        {
            float deltaTime = Time.deltaTime;

            _dasher.Tik(deltaTime);
            _state.Execute(deltaTime);
        }

        public void Construct(IInputService input)
        {
            _input = input;


            _states = new Dictionary<Type, PlayerStateBase>()
            {
                [typeof(SimpleState)] = new SimpleState(this),
                [typeof(InDashState)] = new InDashState(this),
            };

            TransitionTo<SimpleState>();
        }

        private void TransitionTo<TPlayerState>() where TPlayerState : PlayerStateBase
        {
            _state?.Exit();
            _state = _states[typeof(TPlayerState)];

            _state.Enter();
        }

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
                Vector3 movementVector = new Vector3(Controller._input.Axis.x, 0, Controller._input.Axis.y);

                Controller._rotator.RotateIn(movementVector.normalized, deltaTime);
                Controller._mover.Move(movementVector, deltaTime);
                Controller._viewer.PlayMove(Controller._mover.SpeedVectorMagnitude);

                CheckNeedAndDoTransitions();
            }

            public override void Exit()
            {
            }

            protected override bool CheckNeedAndDoTransitions()
            {
                if (Controller._input.IsDashButtonDown && Controller._dasher.CanDash)
                {
                    Controller.TransitionTo<InDashState>();
                    return true;
                }

                return false;
            }
        }

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
    }
}