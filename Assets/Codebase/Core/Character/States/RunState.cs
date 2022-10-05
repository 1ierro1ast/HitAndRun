using Codebase.Infrastructure.Services.Abilities;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Core.Character.States
{
    public class RunState : IUpdatableState
    {
        private readonly CharacterStateMachine _characterStateMachine;
        private readonly Transform _transform;
        private readonly CharacterController _characterController;
        private readonly IShiftImpulseService _shiftImpulseService;

        public RunState(CharacterStateMachine characterStateMachine, Transform transform,
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
            
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _shiftImpulseService.Shift(_characterController, _transform);
            }
        }
    }
}