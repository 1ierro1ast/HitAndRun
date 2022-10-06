using System.Collections;
using Codebase.Core.Settings;
using Codebase.Core.UI;
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

        public MatchRestartState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain,
            IAssetProvider assetProvider, ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _coroutineRunner = coroutineRunner;

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
            _gameStateMachine.Enter<GameplayState>();
        }
    }
}