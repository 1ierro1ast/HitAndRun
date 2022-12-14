using Codebase.Core.Networking;
using Codebase.Infrastructure.Services.AssetManagement;

namespace Codebase.Infrastructure.Services.Factories
{
    public class NetworkFactory : INetworkFactory
    {
        private readonly IAssetProvider _assetProvider;
        private CustomNetworkRoomManager _networkManager;

        public NetworkFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public CustomNetworkRoomManager GetNetworkManager()
        {
            if (_networkManager == null)
                _networkManager = _assetProvider.Instantiate<CustomNetworkRoomManager>(AssetPath.NetworkManagerPath);
            return _networkManager;
        }
    }
}