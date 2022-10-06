using System;
using System.Collections.Generic;
using Codebase.Core.Character.States;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Abilities;
using Codebase.Infrastructure.Services.Input;
using Codebase.Infrastructure.StateMachine;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class CharacterStateMachine : BaseStateMachine
    {
        public CharacterStateMachine(CharacterController characterController, Transform transform,
            AllServices container)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(RunState)] = new RunState(this, container.Single<IInputService>()),
                
                [typeof(ShiftImpulseState)] = new ShiftImpulseState(this, transform, characterController,
                    container.Single<IShiftImpulseService>())
            };
        }

        public bool CompareState<T>() where T : IState
        {
            return _activeState is T;
        }
    }
}