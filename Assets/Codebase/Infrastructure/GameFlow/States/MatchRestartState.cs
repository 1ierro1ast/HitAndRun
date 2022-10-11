using System.Collections;
using Codebase.Core.Settings;
using Codebase.Core.UI;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class MatchRestartState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameplaySettings _gameplaySettings;
        private readonly IEventBus _eventBus;

        public MatchRestartState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain,
            IAssetProvider assetProvider, ICoroutineRunner coroutineRunner, IEventBus eventBus)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _coroutineRunner = coroutineRunner;
            _eventBus = eventBus;

            _gameplaySettings = assetProvider.GetScriptableObject<GameSettings>(AssetPath.GameSettingsPath)
                .GameplaySettings;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _loadingCurtain.OpenPopup();
            _coroutineRunner.StartCoroutine(MatchRespawnCoroutine());
        }

        private IEnumerator MatchRespawnCoroutine()
        {
            yield return new WaitForSeconds(_gameplaySettings.MatchCooldown);
            _eventBus.BroadcastRespawn();
            _gameStateMachine.Enter<GameplayState>();
        }
    }
}