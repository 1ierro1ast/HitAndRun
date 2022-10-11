using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Abilities;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.Input;
using Codebase.Infrastructure.Services.NameSystem;
using Codebase.Infrastructure.Services.Spawn;
using Codebase.Infrastructure.StateMachine;

namespace Codebase.Infrastructure.GameFlow.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices allServices,
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = allServices;
            _coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        public void Enter()
        {
            EnterLoadLevel();
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadGameState, string>("MenuScene");
        }

        private void RegisterServices()
        {
            RegisterAssetProvider();

            RegisterNameService();
            RegisterSpawnPointsStorage();
            RegisterLevelFactory();
            RegisterEventBus();
            RegisterInputService();
            RegisterNetworkFactory();
            RegisterFinishGameHandler();
            RegisterUiFactory();
            RegisterShiftImpulseService();
        }

        private void RegisterNameService()
        {
            _services.RegisterSingle<INameService>(new NameService(_services.Single<IAssetProvider>()));
        }

        private void RegisterFinishGameHandler()
        {
            _services.RegisterSingle<IFinishGameHandler>(new FinishGameHandler(_services.Single<IAssetProvider>(),
                _services.Single<IEventBus>()));
        }

        private void RegisterLevelFactory()
        {
            _services.RegisterSingle<ILevelFactory>(new LevelFactory(_services.Single<IAssetProvider>(),
                _services.Single<ISpawnPointsStorage>()));
        }

        private void RegisterSpawnPointsStorage()
        {
            _services.RegisterSingle<ISpawnPointsStorage>(new SpawnPointsStorage());
        }

        private void RegisterInputService()
        {
            _services.RegisterSingle<IInputService>(new InputService(_coroutineRunner));
        }

        private void RegisterNetworkFactory()
        {
            _services.RegisterSingle<INetworkFactory>(new NetworkFactory(_services.Single<IAssetProvider>()));
        }

        private void RegisterShiftImpulseService()
        {
            _services.RegisterSingle<IShiftImpulseService>(new ShiftImpulseService(
                _services.Single<IAssetProvider>(), _coroutineRunner));
        }

        private void RegisterAssetProvider()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        }

        private void RegisterEventBus()
        {
            _services.RegisterSingle<IEventBus>(
                new EventBus());
        }

        private void RegisterUiFactory()
        {
            _services.RegisterSingle<IUiFactory>(
                new UiFactory(_services.Single<IAssetProvider>(), _services.Single<INetworkFactory>()));
        }
    }
}