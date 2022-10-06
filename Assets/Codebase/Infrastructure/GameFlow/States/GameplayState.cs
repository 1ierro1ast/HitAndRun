using Codebase.Core.UI;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _loadingCurtain;

        public GameplayState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
        }
        public void Exit()
        {
            
        }

        public void Enter()
        {
            _loadingCurtain.ClosePopup();
        }
    }
}