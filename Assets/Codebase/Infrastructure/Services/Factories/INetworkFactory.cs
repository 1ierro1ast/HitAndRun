using Codebase.Core.Networking;

namespace Codebase.Infrastructure.Services.Factories
{
    public interface INetworkFactory : IService
    {
        CustomNetworkManager GetNetworkManager();
    }
}