using Mirror;

namespace Codebase.Infrastructure.Services.Factories
{
    public interface INetworkFactory : IService
    {
        NetworkRoomManager GetNetworkManager();
    }
}