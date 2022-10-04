using Codebase.Core.UI;
using Codebase.Infrastructure.GameFlow;
using Codebase.Infrastructure.GameFlow.States;
using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain LoadingCurtain;
        public GameStateMachine StateMachine;

        private void Awake()
        {
            DontDestroyOnLoad(LoadingCurtain);
            StateMachine = new GameStateMachine(new SceneLoader(this), LoadingCurtain, AllServices.Container,
                this);
            StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}