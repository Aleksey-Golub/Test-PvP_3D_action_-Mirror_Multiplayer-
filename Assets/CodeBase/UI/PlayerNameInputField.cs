using Assets.CodeBase.Infrastructure;
using TMPro;
using UnityEngine;

namespace Assets.CodeBase.UI
{
    public class PlayerNameInputField : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;

        private void Start()
        {
            _inputField.onEndEdit.AddListener(SetPlayerName);
        }

        private void SetPlayerName(string newName)
        {
            var players = FindObjectsOfType<RoomPlayer>();

            foreach (var player in players)
            {
                if (player.isLocalPlayer)
                {
                    player.CmdChangeName(newName);
                    return;
                }
            }
        }
    }
}
