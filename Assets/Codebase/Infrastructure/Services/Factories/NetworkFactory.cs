using Codebase.Core.Networking;
using Codebase.Infrastructure.Services.AssetManagement;

namespace Codebase.Infrastructure.Services.Factories
{
    public class NetworkFactory : INetworkFactory
    {
        private readonly IAssetProvider _assetProvider;
        private CustomNetworkManager _networkManager;

        public NetworkFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public CustomNetworkManager GetNetworkManager()
        {
            if (_networkManager == null)
                _networkManager = _assetProvider.Instantiate<CustomNetworkManager>(AssetPath.NetworkManagerPath);
            return _networkManager;
        }
    }
}