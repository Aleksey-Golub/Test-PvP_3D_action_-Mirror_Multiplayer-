using Assets.CodeBase.CameraLogic;
using Assets.CodeBase.Player;
using Assets.CodeBase.Services.Input;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private CameraMover _cameraMover;
        
        private IInputService _input;

        private void Start()
        {
            RegisterServices();

            _player.Construct(_input);
            _cameraMover.Construct(_input);
        }

        private void RegisterServices()
        {
            _input = new DesktopInputService();
        }
    }
}