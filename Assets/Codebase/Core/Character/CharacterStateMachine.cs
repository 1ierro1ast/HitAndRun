using System;
using System.Collections.Generic;
using Codebase.Core.Character.States;
using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Abilities;
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
                [typeof(RunState)] = new RunState(this, transform, characterController,
                    container.Single<IShiftImpulseService>()),
                [typeof(ShiftImpulseState)] = new ShiftImpulseState(this)
            };
        }

        public void Update()
        {
            if (_activeState is IUpdatableState) (_activeState as IUpdatableState)?.Update();
        }
    }
}