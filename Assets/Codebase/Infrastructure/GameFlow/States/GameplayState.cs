using System.Collections;
using Codebase.Core.UI;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IEventBus _eventBus;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ILevelFactory _levelFactory;

        public GameplayState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain, IEventBus eventBus,
            ICoroutineRunner coroutineRunner, ILevelFactory levelFactory)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _eventBus = eventBus;
            _coroutineRunner = coroutineRunner;
            _levelFactory = levelFactory;
        }

        public void Exit()
        {
            _eventBus.LevelFinishedEvent -= EventBus_OnLevelFinishedEvent;
        }

        public void Enter()
        {
            _levelFactory.GetLevel();
            _coroutineRunner.StartCoroutine(StartGameplayCoroutine());
            _eventBus.LevelFinishedEvent += EventBus_OnLevelFinishedEvent;
        }

        private void EventBus_OnLevelFinishedEvent(string winnerName)
        {
            _loadingCurtain.SetWinnerView(winnerName);
            _gameStateMachine.Enter<MatchRestartState>();
        }

        private IEnumerator StartGameplayCoroutine()
        {
            yield return new WaitForSeconds(0.25f);
            _eventBus.BroadcastGamePlayStart();
            _loadingCurtain.ClosePopup();
        }
    }
}