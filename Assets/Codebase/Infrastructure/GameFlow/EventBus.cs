using System;

namespace Codebase.Infrastructure.GameFlow
{
    public class EventBus : IEventBus
    {
        public event Action LevelLoadedEvent;
        public event Action GamePlayStartEvent;
        public event Action<string> LevelFinishedEvent;
        public event Action ReadyToGameEvent;

        public void BroadcastLevelLoaded()
        {
            LevelLoadedEvent?.Invoke();
        }

        public void BroadcastGamePlayStart()
        {
            GamePlayStartEvent?.Invoke();
        }

        public void BroadcastLevelFinished(string winnerName)
        {
            LevelFinishedEvent?.Invoke(winnerName);
        }

        public void BroadcastReadyToGame()
        {
            ReadyToGameEvent?.Invoke();
        }
    }
}