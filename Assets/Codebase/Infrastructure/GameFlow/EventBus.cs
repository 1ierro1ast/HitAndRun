using System;

namespace Codebase.Infrastructure.GameFlow
{
    public class EventBus : IEventBus
    {
        public event Action GamePlayStartEvent;
        public event Action<string> LevelFinishedEvent;
        public event Action RespawnEvent;

        public void BroadcastGamePlayStart() => GamePlayStartEvent?.Invoke();

        public void BroadcastLevelFinished(string winnerName) => LevelFinishedEvent?.Invoke(winnerName);

        public void BroadcastRespawn() => RespawnEvent?.Invoke();
    }
}