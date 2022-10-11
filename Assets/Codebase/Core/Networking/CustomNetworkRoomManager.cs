using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Spawn;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Networking
{
    public class CustomNetworkRoomManager : NetworkRoomManager
    {
        private ISpawnPointsStorage _spawnPointStorage;

        public override void Awake()
        {
            _spawnPointStorage = AllServices.Container.Single<ISpawnPointsStorage>();
            base.Awake();
        }

        public override void OnRoomServerPlayersReady()
        {
            NetworkServer.SendToAll(new MatchStart());
            
            base.OnRoomServerPlayersReady();
        }

        public override Transform GetStartPosition()
        {
            return _spawnPointStorage.GetSpawnPoint();
        }
    }
}