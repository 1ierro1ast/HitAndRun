using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Abilities;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.SaveLoad;
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
            _stateMachine.Enter<LoadLevelState, string>("GameScene");
        }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            RegisterSaveLoadService();

            RegisterEventBus();
            RegisterUiFactory();
            RegisterShiftImpulseService();
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

        private void RegisterSaveLoadService()
        {
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService());
        }

        private void RegisterEventBus()
        {
            _services.RegisterSingle<IEventBus>(
                new EventBus());
        }

        private void RegisterUiFactory()
        {
            _services.RegisterSingle<IUiFactory>(
                new UiFactory(_services.Single<IAssetProvider>()));
        }
    }
}