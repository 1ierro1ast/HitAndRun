using Codebase.Core.Networking;
using Codebase.Infrastructure.Services.AssetManagement;
using Mirror;

namespace Codebase.Infrastructure.Services.Factories
{
    public class NetworkFactory : INetworkFactory
    {
        private readonly IAssetProvider _assetProvider;
        private NetworkRoomManager _networkManager;

        public NetworkFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public NetworkRoomManager GetNetworkManager()
        {
            if (_networkManager == null)
                _networkManager = _assetProvider.Instantiate<NetworkRoomManager>(AssetPath.NetworkManagerPath);
            return _networkManager;
        }
    }
}