using Codebase.Infrastructure.Services.AssetManagement;
using Mirror;

namespace Codebase.Infrastructure.Services.Factories
{
    public class NetworkFactory : INetworkFactory
    {
        private readonly IAssetProvider _assetProvider;
        private NetworkManager _networkManager;

        public NetworkFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public NetworkManager GetNetworkManager()
        {
            if (_networkManager == null)
                _networkManager = _assetProvider.Instantiate<NetworkManager>(AssetPath.NetworkManagerPath);
            return _networkManager;
        }
    }
}