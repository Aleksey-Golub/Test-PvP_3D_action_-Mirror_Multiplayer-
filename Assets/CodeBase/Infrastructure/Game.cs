using Assets.CodeBase.CameraLogic;
using Assets.CodeBase.Player;
using Assets.CodeBase.Services.InputService;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private CameraController _cameraController;
        
        private IInputService _input;

        private void Start()
        {
            RegisterServices();

            _player.Construct(_input, _cameraController.Camera);
            _cameraController.Construct(_player.transform, _input);
        }

        private void RegisterServices()
        {
            _input = new DesktopInputService();
        }
    }
}