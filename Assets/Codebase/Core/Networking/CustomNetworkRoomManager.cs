using System;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Spawn;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Networking
{
    public class CustomNetworkRoomManager : NetworkRoomManager
    {
        private ISpawnPointsStorage _spawnPointStorage;
        private IEventBus _eventBus;

        public override void Awake()
        {
            _spawnPointStorage = AllServices.Container.Single<ISpawnPointsStorage>();
            _eventBus = AllServices.Container.Single<IEventBus>();
            _eventBus.LevelFinishedEvent += EventBus_OnLevelFinishedEvent;
            base.Awake();
        }

        private void EventBus_OnLevelFinishedEvent(string obj)
        {
            
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