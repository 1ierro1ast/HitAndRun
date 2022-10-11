using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.GameFlow
{
    public interface IEventBus : IService
    {
        event Action GamePlayStartEvent;
        event Action<string> LevelFinishedEvent;
        event Action RespawnEvent;
        
        void BroadcastGamePlayStart();
        void BroadcastLevelFinished(string winnerName);
        void BroadcastRespawn();
    }
}