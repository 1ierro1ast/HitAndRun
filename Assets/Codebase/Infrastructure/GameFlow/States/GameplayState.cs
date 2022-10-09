using System.Collections;
using Codebase.Core.Networking;
using Codebase.Core.UI;
using Codebase.Infrastructure.StateMachine;
using Mirror;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IEventBus _eventBus;
        private readonly ICoroutineRunner _coroutineRunner;

        public GameplayState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain, IEventBus eventBus,
            ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _eventBus = eventBus;
            _coroutineRunner = coroutineRunner;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _coroutineRunner.StartCoroutine(StartGameplayCoroutine());
        }

        private IEnumerator StartGameplayCoroutine()
        {
            yield return new WaitForSeconds(0.25f);
            NetworkClient.RegisterHandler<MatchEnd>(OnMatchEnd);
            _eventBus.BroadcastGamePlayStart();
            _loadingCurtain.ClosePopup();
        }

        private void OnMatchEnd(MatchEnd message)
        {
            NetworkClient.UnregisterHandler<MatchEnd>();
            _loadingCurtain.SetWinnerView(message.WinnerName);
            _gameStateMachine.Enter<MatchRestartState>();
        }
    }
}