using System;
using System.Collections.Generic;
using Codebase.Core.UI;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow
{
    public class GameStateMachine : BaseStateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices container,
            ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, container, coroutineRunner),

                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader, loadingCurtain,
                    container.Single<INetworkFactory>()),

                [typeof(MainMenuState)] = new MainMenuState(this, loadingCurtain, container.Single<IUiFactory>()),

                [typeof(LobbyState)] = new LobbyState(this, container.Single<IUiFactory>(), loadingCurtain),

                [typeof(GameplayState)] = new GameplayState(this, loadingCurtain, container.Single<IEventBus>(),
                    coroutineRunner, container.Single<ILevelFactory>()),

                [typeof(MatchRestartState)] = new MatchRestartState(this, loadingCurtain,
                    container.Single<IAssetProvider>(), coroutineRunner, container.Single<IEventBus>())
            };
        }
    }
}