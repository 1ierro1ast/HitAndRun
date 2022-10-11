namespace Codebase.Infrastructure.Services.NameSystem
{
    public interface INameService : IService
    {
        string PlayerName { get; }
        int MaxPlayerNameLength { get; }
        void SetPlayerName(string name);
    }
}