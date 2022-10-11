using Codebase.Core.Networking;
using Codebase.Core.UI;
using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;
using Mirror;
using UnityEngine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LobbyState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiFactory _uiFactory;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IEventBus _eventBus;

        private RoomPopup _roomPopup;

        public LobbyState(GameStateMachine gameStateMachine, IUiFactory uiFactory, LoadingCurtain loadingCurtain,
            IEventBus eventBus)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
            _loadingCurtain = loadingCurtain;
            _eventBus = eventBus;
        }

        public void Exit()
        {
            _roomPopup.ClosePopup();
        }

        public void Enter()
        {
            _roomPopup = _uiFactory.CreateRoomPopup();
            _roomPopup.OpenPopup();
            _loadingCurtain.ClosePopup();
            NetworkClient.RegisterHandler<MatchStart>(OnMatchStart);
        }

        private void OnMatchStart(MatchStart obj)
        {
            _gameStateMachine.Enter<GameplayState>();
        }
    }
}