using Assets.CodeBase.Player;
using Mirror;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class NetManager : NetworkRoomManager
    {
        bool _showStartButton;

        public override void OnRoomServerPlayersReady()
        {
            // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
            _showStartButton = true;
#endif
        }

        public override void OnGUI()
        {
            base.OnGUI();

            if (allPlayersReady && _showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
            {
                // set to false to hide it in the game scene
                _showStartButton = false;

                ServerChangeScene(GameplayScene);
            }
        }

        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            PlayerController playerController = gamePlayer.GetComponent<PlayerController>();
            playerController.PlayerName = roomPlayer.GetComponent<RoomPlayer>().PlayerName;
            return true;
        }
    }
}