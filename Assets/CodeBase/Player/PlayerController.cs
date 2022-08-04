using Assets.CodeBase.Logic;
using Assets.CodeBase.Logic.CharacterComponents;
using Assets.CodeBase.Services.InputService;
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
        [SerializeField] private float _immortalDuration = 3f;

        [Header("Debug")]
        [SerializeField] private bool _isPlayer;

        public bool IsImmortal { get; private set; }

        private Timer _immortalTimer = new Timer();
        private IInputService _input;
        private Camera _camera;
        private Vector3 _normalizedMovementVector;
        private PlayerStateBase _state;
        private Dictionary<Type, PlayerStateBase> _states;

        private void Start()
        {
            if (_isPlayer == false)
                Construct(new FakeInput(), Camera.main);
        }

        public void Update()
        {
            float deltaTime = Time.deltaTime;

            if (IsImmortal)
                UpdateAndCheckImmortality(deltaTime);

            CalculateNormalizedMovementVector();
            _dasher.Tik(deltaTime);
            _state.Execute(deltaTime);
        }

        public void Construct(IInputService input, Camera camera)
        {
            _input = input;
            _camera = camera;

            _states = new Dictionary<Type, PlayerStateBase>()
            {
                [typeof(SimpleState)] = new SimpleState(this),
                [typeof(InDashState)] = new InDashState(this),
            };

            TransitionTo<SimpleState>();
        }

        public void TakeDamage()
        {
            print(gameObject.name + " TakeDamage");
            IsImmortal = true;
            _viewer.SetDamaged(true);
        }

        private void TransitionTo<TPlayerState>() where TPlayerState : PlayerStateBase
        {
            _state?.Exit();
            _state = _states[typeof(TPlayerState)];

            _state.Enter();
        }

        private void CalculateNormalizedMovementVector()
        {
            _normalizedMovementVector = _camera.transform.TransformDirection(_input.MoveAxis);
            _normalizedMovementVector.y = 0;
            _normalizedMovementVector.Normalize();
        }

        private void UpdateAndCheckImmortality(float deltaTime)
        {
            _immortalTimer.Tik(deltaTime);
            if (_immortalTimer.Value >= _immortalDuration)
            {
                _immortalTimer.Reset();
                IsImmortal = false;
                _viewer.SetDamaged(false);
            }
        }
    }
}