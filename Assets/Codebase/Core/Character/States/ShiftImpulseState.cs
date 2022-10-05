using Codebase.Infrastructure.StateMachine;

namespace Codebase.Core.Character.States
{
    public class ShiftImpulseState : IUpdatableState
    {
        private readonly CharacterStateMachine _characterStateMachine;

        public ShiftImpulseState(CharacterStateMachine characterStateMachine)
        {
            _characterStateMachine = characterStateMachine;
        }
        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}