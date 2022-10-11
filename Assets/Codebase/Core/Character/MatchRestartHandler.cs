using Codebase.Core.Scores;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Spawn;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class MatchRestartHandler : NetworkBehaviour
    {
        [SerializeField] private MoveController _moveController;
        [SerializeField] private ScoreCounter _scoreCounter;
        private IEventBus _eventBus;
        private ISpawnPointsStorage _spawnPointsStorage;
        private void Awake()
        {
            _spawnPointsStorage = AllServices.Container.Single<ISpawnPointsStorage>();
            _eventBus = AllServices.Container.Single<IEventBus>();
            _eventBus.RespawnEvent += EventBus_OnRespawnEvent;
        }

        private void OnDestroy()
        {
            _eventBus.RespawnEvent -= EventBus_OnRespawnEvent;
        }

        private void EventBus_OnRespawnEvent()
        {
            _scoreCounter.CleanScores();
            _moveController.MoveToSpawnPoint(_spawnPointsStorage.GetSpawnPoint().position);
        }
    }
}