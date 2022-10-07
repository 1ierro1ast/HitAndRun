using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Networking
{
    public class CustomNetworkManager : NetworkManager
    {
        private IEventBus _eventBus;
        
        public override void Awake()
        {
            base.Awake();
            _eventBus = AllServices.Container.Single<IEventBus>();
        }
        
        public override void OnStartServer()
        {
            base.OnStartServer();
            Debug.Log(nameof(OnStartServer));
            NetworkClient.RegisterHandler<MatchEnd>(OnMatchEnd);
        }

        private void OnMatchEnd(MatchEnd obj)
        {
            _eventBus.BroadcastLevelFinished(obj.PlayerName);
        }
    }
}
