using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IUiFactory _uiFactory;

        private StartPopup _startPopup;

        public MainMenuState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain, IUiFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
        }

        public void Exit()
        {
            _startPopup.StartButtonClickEvent -= StartPopup_OnStartButtonClickEvent;
            _startPopup.ClosePopup();
        }

        public void Enter()
        {
            _loadingCurtain.ClosePopup();
            _startPopup = _uiFactory.CreateStartPopup();
            _startPopup.StartButtonClickEvent += StartPopup_OnStartButtonClickEvent;
        }

        private void StartPopup_OnStartButtonClickEvent()
        {
            _loadingCurtain.OpenPopup();
            _gameStateMachine.Enter<LobbyState>();
        }
    }
}