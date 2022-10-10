using System;
using Mirror;

namespace Codebase.Core.Networking
{
    public class CustomNetworkRoomManager : NetworkRoomManager
    {
        public event Action AllPlayersReady; 
        public override void OnRoomServerPlayersReady()
        {
            base.OnRoomServerPlayersReady();
            AllPlayersReady?.Invoke();
        }
    }
}
