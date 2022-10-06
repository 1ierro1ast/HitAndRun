using System;

namespace Codebase.Infrastructure.Services.NameSystem
{
    public interface INameService : IService
    {
        event Action<string> NameChanged;
        string PlayerName { get; }
        int MaxPlayerNameLength { get; }
        void SetPlayerName(string name);
    }
}