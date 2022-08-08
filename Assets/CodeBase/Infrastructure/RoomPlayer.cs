using Mirror;

namespace Assets.CodeBase.Infrastructure
{
    public class RoomPlayer : NetworkRoomPlayer
    {
        [SyncVar]
        public string PlayerName;

        [Command]
        internal void CmdChangeName(string newName)
        {
            PlayerName = newName;
        }
    }
}
