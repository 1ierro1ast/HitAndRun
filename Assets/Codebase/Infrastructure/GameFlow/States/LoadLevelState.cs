using Codebase.Core.UI;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly INetworkFactory _networkFactory;
        private readonly ILevelFactory _levelFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            INetworkFactory networkFactory, ILevelFactory levelFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _networkFactory = networkFactory;
            _levelFactory = levelFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.OpenPopup();
            _networkFactory.GetNetworkManager();
            _sceneLoader.LoadScene(sceneName, false, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _levelFactory.GetLevel();
            _gameStateMachine.Enter<MainMenuState>();
        }
    }
}