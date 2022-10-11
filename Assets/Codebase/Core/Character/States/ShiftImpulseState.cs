using Codebase.Infrastructure.Services.Abilities;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Core.Character.States
{
    public class ShiftImpulseState : IState
    {
        private readonly CharacterStateMachine _characterStateMachine;
        private readonly Transform _transform;
        private readonly CharacterController _characterController;
        private readonly IShiftImpulseService _shiftImpulseService;

        public ShiftImpulseState(CharacterStateMachine characterStateMachine, Transform transform,
            CharacterController characterController, IShiftImpulseService shiftImpulseService)
        {
            _characterStateMachine = characterStateMachine;
            _transform = transform;
            _characterController = characterController;
            _shiftImpulseService = shiftImpulseService;
        }
        public void Exit()
        {
        }

        public void Enter()
        {
            _shiftImpulseService.Shift(_characterController, _transform, OnImpulseFinishedCallback);
        }

        private void OnImpulseFinishedCallback()
        {
            _characterStateMachine.Enter<RunState>();
        }
    }
}