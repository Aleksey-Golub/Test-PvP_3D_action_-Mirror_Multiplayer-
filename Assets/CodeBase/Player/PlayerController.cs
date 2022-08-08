using Assets.CodeBase.Infrastructure;
using Assets.CodeBase.Logic;
using Assets.CodeBase.Logic.CharacterComponents;
using Assets.CodeBase.Services.InputService;
using Assets.CodeBase.UI;
using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Player
{
    public partial class PlayerController : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private MoverBase _mover;
        [SerializeField] private RotatorBase _rotator;
        [SerializeField] private CharacterViewer _viewer;
        [SerializeField] private Dasher _dasher;

        [Header("Settings")]
        
        [SerializeField] private float _immortalDuration = 3f;
        [SyncVar]
        public string PlayerName;
        [SyncVar(hook = nameof(OnScoreChanged))]
        private int _score;

        private ScoreText _scoreText;
        [SyncVar(hook = nameof(OnIsDamagedChanged))]
        private bool _isDamaged;
        private Timer _immortalTimer = new Timer();
        private IInputService _input;
        private Camera _camera;
        private Vector3 _normalizedMovementVector;
        private Vector3 _startPosition;
        private PlayerStateBase _state;
        private Dictionary<Type, PlayerStateBase> _states;
        private Game _game;

        public bool IsDamaged => _isDamaged;
        public int Score => _score;

        public event Action<PlayerController> LocalPlayerStoped;
        public event Action<PlayerController> StopClienHappened;
        public event Action<PlayerController> ScoreChanged;

        public void Update()
        {
            float deltaTime = Time.deltaTime;

            if (isServer && _isDamaged)
                UpdateAndCheckIsDamaged(deltaTime);

            if (isLocalPlayer == false)
                return;

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

        public void SetScoreText(ScoreText scoreText)
        {
            _scoreText = scoreText;
        }

        public void TakeDamage()
        {
            _isDamaged = true;
        }

        public void IncreaseScore()
        {
            _score++;
            ScoreChanged?.Invoke(this);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            _game = FindObjectOfType<Game>();
            _game.OnStartClientPlayer(this);

            _scoreText.SetScoreFor(PlayerName, _score);

        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            StopClienHappened?.Invoke(this);
            _scoreText.Die();
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            _game.OnStartLocalPlayer(this);
        }

        public override void OnStopLocalPlayer()
        {
            base.OnStopLocalPlayer();

            LocalPlayerStoped?.Invoke(this);
        }

        [ClientRpc]
        internal void RpcRestart(Vector3 position)
        {
            transform.position = position;

            if (isLocalPlayer)
                CmdResetScore();
        }

        [Command]
        private void CmdResetScore()
        {
            _score = 0;
        }

        private void OnScoreChanged(int oldValue, int newValue)
        {
            _scoreText.SetScoreFor(PlayerName, _score);
        }

        private void OnIsDamagedChanged(bool oldValue, bool newValue)
        {
            _viewer.SetDamaged(newValue);
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

        private void UpdateAndCheckIsDamaged(float deltaTime)
        {
            _immortalTimer.Tik(deltaTime);
            if (_immortalTimer.Value >= _immortalDuration)
            {
                _immortalTimer.Reset();
                _isDamaged = false;
            }
        }
    }
}