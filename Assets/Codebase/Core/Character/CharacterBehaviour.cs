using Codebase.Infrastructure.Services;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class CharacterBehaviour : MonoBehaviour, ITagable
    {
        private CharacterStateMachine _characterStateMachine;
        private CharacterController _characterController;

        private void Awake()
        {
            _characterStateMachine = new CharacterStateMachine(_characterController, transform, AllServices.Container);
        }

        private void Update()
        {
            _characterStateMachine.Update();
        }

        public void Tag()
        {
            Debug.Log("TAGGED");
        }
    }
}