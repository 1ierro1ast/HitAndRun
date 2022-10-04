using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.GameFlow
{
    public interface IEventBus : IService
    {
        event Action LevelLoadedEvent;
        event Action GamePlayStartEvent;
        event Action LevelFinishedEvent;

        void BroadcastLevelLoaded();
        void BroadcastGamePlayStart();
        void BroadcastLevelFinished();
    }
}