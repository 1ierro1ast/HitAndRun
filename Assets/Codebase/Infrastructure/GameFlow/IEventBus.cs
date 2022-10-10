﻿using System;
using Codebase.Infrastructure.Services;

namespace Codebase.Infrastructure.GameFlow
{
    public interface IEventBus : IService
    {
        event Action LevelLoadedEvent;
        event Action GamePlayStartEvent;
        event Action<string> LevelFinishedEvent;
        event Action ReadyToGameEvent;

        void BroadcastLevelLoaded();
        void BroadcastGamePlayStart();
        void BroadcastLevelFinished(string winnerName);
        void BroadcastReadyToGame();
    }
}