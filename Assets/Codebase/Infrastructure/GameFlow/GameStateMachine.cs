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

                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain,
                    container.Single<INetworkFactory>(), container.Single<ILevelFactory>()),

                [typeof(MainMenuState)] = new MainMenuState(this, loadingCurtain, container.Single<IUiFactory>()),

                [typeof(GameplayState)] = new GameplayState(this, loadingCurtain),

                [typeof(MatchRestartState)] = new MatchRestartState(this, loadingCurtain,
                    container.Single<IAssetProvider>(), coroutineRunner)
            };
        }
    }
}