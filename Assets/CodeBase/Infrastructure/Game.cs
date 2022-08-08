using Assets.CodeBase.CameraLogic;
using Assets.CodeBase.Player;
using Assets.CodeBase.Services.InputService;
using Assets.CodeBase.UI;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class Game : NetworkBehaviour
    {
        [SerializeField] private int _scoreForWin = 3;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private UIManager _uiManager;

        private readonly List<PlayerController> _players = new List<PlayerController>();

        private IInputService _input;
        private PlayerController _playerController;
        private Coroutine _coroutine;

        private void Awake()
        {
            RegisterServices();

            _uiManager.Construct();

            _cameraController.Construct(_input);
        }

        private void Update()
        {
            if (_playerController != null)
            {
                if (_input.IsMainPointerButtonDown)
                    _coroutine = StartCoroutine(nameof(DisableCursor));

                if (_input.IsEscButtonDown)
                    EnableCursor();
            }
        }

        public void OnStartLocalPlayer(PlayerController player)
        {
            _playerController = player;
            _playerController.Construct(_input, _cameraController.Camera);
            _cameraController.SetTarget(_playerController.transform);

            player.LocalPlayerStoped += OnLocalPlayerStoped;
            _coroutine = StartCoroutine(nameof(DisableCursor));
        }

        internal void OnStartClientPlayer(PlayerController player)
        {
            player.SetScoreText(_uiManager.GetScoreText());

            if (isServer == false)
                return;

            _players.Add(player);
            player.ScoreChanged += OnScoreChanged;
            player.StopClienHappened += OnStopClientHappened;
        }

        private void OnStopClientHappened(PlayerController player)
        {
            player.StopClienHappened -= OnStopClientHappened;
            player.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(PlayerController player)
        {
            if (isServer == false)
                return;

            if (player.Score >= _scoreForWin)
                StartCoroutine(RestartGame(player.PlayerName));
        }

        private IEnumerator RestartGame(string playerName)
        {
            _uiManager.RpcShowWinScreen(playerName);

            yield return new WaitForSeconds(5f);

            _uiManager.RpcHideWinScreen();

            foreach (var player in _players)
                player.RpcRestart(NetManager.singleton.GetStartPosition().position);
        }

        private void OnLocalPlayerStoped(PlayerController player)
        {
            player.LocalPlayerStoped -= OnLocalPlayerStoped;
            _playerController = null;

            StopCoroutine(_coroutine);
            EnableCursor();
        }

        private void RegisterServices()
        {
            _input = new DesktopInputService();
        }

        private IEnumerator DisableCursor()
        {
            yield return new WaitForSeconds(0.5f);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void EnableCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}