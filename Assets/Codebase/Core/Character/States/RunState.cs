using Codebase.Infrastructure.Services.Abilities;
using Codebase.Infrastructure.Services.Input;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Core.Character.States
{
    public class RunState : IState, ISupportTagState
    {
        private readonly CharacterStateMachine _characterStateMachine;
        private readonly IInputService _inputService;

        public RunState(CharacterStateMachine characterStateMachine, IInputService inputService)
        {
            _characterStateMachine = characterStateMachine;
            _inputService = inputService;
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
            _characterStateMachine.Enter<ShiftImpulseState>();
        }

        public void Tag()
        {
            throw new System.NotImplementedException();
        }
    }
}