using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.Input;
using Mirror;
using UnityEngine;

namespace Codebase.Core.Character
{
    public class AnimationsHandler : NetworkBehaviour
    {
        [SerializeField] private Animator _animator;
        private IInputService _inputService;
        private bool _cachedMovingFlag;
        private bool _cachedRunningFlag;
        
        private static readonly int IsMoving = Animator.StringToHash("IsWalking");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (!hasAuthority) return;
            
            if(_cachedMovingFlag != _inputService.IsMoving) 
                SetMove(_inputService.IsMoving);
            if(_cachedRunningFlag != _inputService.IsRunning) 
                SetRunning(_inputService.IsRunning);
        }

        private void SetMove(bool isMoving)
        {
            _cachedMovingFlag = isMoving;
            _animator.SetBool(IsMoving, isMoving);
        }

        private void SetRunning(bool isRunning)
        {
            _cachedRunningFlag = isRunning;
            _animator.SetBool(IsRunning, isRunning);
        }
    }
}