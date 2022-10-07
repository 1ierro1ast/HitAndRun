using Codebase.Infrastructure.Services.Input;
using Codebase.Infrastructure.StateMachine;
using Mirror;

namespace Codebase.Core.Character.States
{
    public class RunState : IState
    {
        private readonly CharacterStateMachine _characterStateMachine;
        private readonly IInputService _inputService;
        private readonly NetworkBehaviour _networkBehaviour;

        public RunState(CharacterStateMachine characterStateMachine, IInputService inputService,
            NetworkBehaviour networkBehaviour)
        {
            _characterStateMachine = characterStateMachine;
            _inputService = inputService;
            _networkBehaviour = networkBehaviour;
        }

        public void Exit()
        {
            _inputService.FireButtonEvent -= InputService_OnFireButtonEvent;
        }

        public void Enter()
        {
            _inputService.FireButtonEvent += InputService_OnFireButtonEvent;
        }

        private void InputService_OnFireButtonEvent()
        {
            if (!_networkBehaviour.isLocalPlayer) return;
            _characterStateMachine.Enter<ShiftImpulseState>();
        }
    }
}